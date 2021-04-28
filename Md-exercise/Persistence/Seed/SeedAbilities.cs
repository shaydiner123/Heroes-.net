using Md_exercise.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence.Seed
{
    public class SeedAbilities
    {
        public void SeedAbilitiesData(HeroesDbContext context)
        {
            if (!context.Abilities.Any())
            {
                context.Abilities.AddRange(new Ability { Name = Ability.Defender },
                new Ability { Name = Ability.Attacker });
                context.SaveChanges();
            }
        }
    }
}
