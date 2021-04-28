using Md_exercise.Core.Services.DbChecker;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence
{

    public class DbChecker : IDbChecker
    {
        private DbContext dbContext;
        private HeroesDbContext context => this.dbContext as HeroesDbContext;
        public DbChecker(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> IsHeroAlreadyTrainedFiveTimesTodayAsync(Guid heroId)
        {
            return await this.context.Heroes.AnyAsync(h => h.Id == heroId && h.TrainingAmountPerformedToday>=5);
        }

        public async Task<bool> IsHeroExistAsync(Guid heroId)
        {
            return await this.context.Heroes.AnyAsync(h => h.Id == heroId);
        }

        public async Task<bool> IsTrainerExistAsync(string trainerId)
        {
            return await this.context.Users.AnyAsync(t => t.Id == trainerId);
        }

        public async Task<bool> IsTrainerHasThisHero(string trainerId,Guid heroId)
        {
            return await this.context.Heroes.AnyAsync(h =>h.Id==heroId && h.TrainerId == trainerId);
        }
    }
}
