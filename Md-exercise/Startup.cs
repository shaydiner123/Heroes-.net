using Md_exercise.Core;
using Md_exercise.Core.Domain;
using Md_exercise.Core.Repositories;
using Md_exercise.Core.Services.DbChecker;
using Md_exercise.Core.Services.Hosted_Services;
using Md_exercise.Core.Services.TokenGenerator;
using Md_exercise.Core.Services.UserAuthManager;
using Md_exercise.Log4net;
using Md_exercise.Middlewares;
using Md_exercise.Persistence;
using Md_exercise.Persistence.Repositories;
using Md_exercise.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Md_exercise
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.Configure<JsonOptions>(opts => {
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Md_exercise", Version = "v1" });
            });


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                                    .AllowAnyMethod();
                    });
            });

            services.AddHostedService<RefreshTrainingAmountHostedService>();

            services.AddAutoMapper(typeof(Startup));

            //dbcontexts
            services.AddDbContext<HeroesDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("HeroesDbConnection"));
            });

            services.AddDbContext<HeroesLogsDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("HeroesLogsDbConnection"));
            });

            services.AddScoped<DbContext, HeroesDbContext>();

            //repositories + unitOfWOrk
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbChecker, DbChecker>();

            //services
            services.AddTransient<IUserAuthManager, UserAuthManager>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<SeedData>();

            //identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<HeroesDbContext>();

            services.Configure<IdentityOptions>(opts => {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.User.RequireUniqueEmail = true;
            });


            //auth
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["Jwt:secret"])),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });



        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedData seedData)
        {
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Md_exercise v1"));
            }

            // app.UseHttpsRedirection();
            app.UseMiddleware<RequesLoggingMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await seedData.EnsurePopulated(app);
        }
    }
}
