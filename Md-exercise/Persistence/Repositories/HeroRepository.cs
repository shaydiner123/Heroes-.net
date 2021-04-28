using Md_exercise.Core.Domain;
using Md_exercise.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence.Repositories
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        private HeroesDbContext context => this.dbContext as HeroesDbContext;
        public HeroRepository(DbContext dbContext) : base(dbContext)
        { }


        public async Task<IEnumerable< Hero>> GetHeroesByAsync(Guid? heroId,string trainerId)
        {
            return await this.context.Heroes
                .Include(h => h.HeroAbility)
                .Include(h=>h.SuitColors)
                .Where(h=>h.Id==heroId || heroId==null)
                .Where(h => h.TrainerId == trainerId)
                .ToListAsync();
        }


        public async Task<Hero> TrainHeroAsync(Guid id)
        {
            Hero hero = await this.context.Heroes
                .Include(h => h.HeroAbility)
                .Include(h => h.SuitColors)
                .FirstOrDefaultAsync(h => h.Id == id);
            Random random = new Random(DateTime.Now.Millisecond);
            double addedPower = random.GetRandomNumberInSpecifiedRange(0, 0.1) * 100;
            hero.CurrentPower += (decimal)addedPower;
            hero.TrainingAmountPerformedToday++;
            return hero;
        }
      
    }
}
