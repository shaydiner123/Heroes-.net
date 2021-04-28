using Md_exercise.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence
{

    public class HeroesDbContext : IdentityDbContext<ApplicationUser>
    {
        public HeroesDbContext(DbContextOptions<HeroesDbContext> options) : base(options) { }

        public DbSet<Hero> Heroes { get; set; }

        public DbSet<Ability> Abilities { get; set; }

        public DbSet<SuitColor> SuitColors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Hero>()
            .HasOne(h => h.HeroAbility)
            .WithMany(a => a.Heroes)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }



}

