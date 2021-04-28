using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Services.DbChecker
{
    public interface IDbChecker
    {
        Task<bool> IsTrainerExistAsync(string trainerId);
        Task<bool> IsHeroExistAsync(Guid heroId);
        Task<bool> IsHeroAlreadyTrainedFiveTimesTodayAsync(Guid heroId);

        Task<bool> IsTrainerHasThisHero(string trainerId, Guid heroId);
    }
}
