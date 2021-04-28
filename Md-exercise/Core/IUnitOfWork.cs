using Md_exercise.Core.Repositories;
using Md_exercise.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core
{
    public interface IUnitOfWork
    {
       HeroesDbContext DbContext { get; }
       IHeroRepository HeroRepo { get; }

       Task<int> SaveAsync();
    }
}
