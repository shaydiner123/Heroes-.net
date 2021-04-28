using Md_exercise.Core.Domain;
using Md_exercise.Log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence.Seed
{
    public class SeedData
    {

         private static readonly log4net.ILog log = LogHelper.GetLogger();

        public async Task EnsurePopulated(IApplicationBuilder app)
        {
            try
            {
                var scope = app.ApplicationServices.CreateScope();
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                HeroesDbContext context = scope.ServiceProvider.GetRequiredService<HeroesDbContext>();
                HeroesLogsDbContext logsContext= scope.ServiceProvider.GetRequiredService<HeroesLogsDbContext>();

                if (logsContext.Database.GetPendingMigrations().Any())
                    logsContext.Database.Migrate();

                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();

                await new SeedUsers().SeedUsersData(context, userManager);
                new SeedAbilities().SeedAbilitiesData(context);
                new SeedSuitColors().SeedSuitColorsData(context);
                new SeedHeroes().SeedHeroesData(context, userManager);
            }
            catch (Exception exe)
            {
                log.Error("something went wrong with saving the initial data", exe);
                Console.WriteLine(exe.Message);
            }
        }

    }

}
