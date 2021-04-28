using Md_exercise.Core;
using Md_exercise.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        private IHeroRepository heroRepo;

        HeroesDbContext IUnitOfWork.DbContext => this.dbContext as HeroesDbContext;


        IHeroRepository IUnitOfWork.HeroRepo => this.heroRepo;

        public UnitOfWork(DbContext dbContext, IHeroRepository heroRepo)
        {
            this.dbContext = dbContext;
            this.heroRepo = heroRepo;
        }



        public async Task<int> SaveAsync() =>
            await dbContext.SaveChangesAsync();

    }
}
