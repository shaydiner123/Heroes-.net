using Md_exercise.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Repositories
{
    public interface IHeroRepository
    {
        Task<Hero> TrainHeroAsync(Guid heroId);
        Task<IEnumerable<Hero>> GetHeroesByAsync(Guid? heroId,string trainerId);
    }
}
