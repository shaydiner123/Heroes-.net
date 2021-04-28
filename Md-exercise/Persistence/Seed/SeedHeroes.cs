using Md_exercise.Core.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence.Seed
{
    public class SeedHeroes
    {
        public void SeedHeroesData(HeroesDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Heroes.Any())
            {
                List<Ability> abilities = context.Abilities.ToList();
                var usersIds = userManager.Users.Select(user => user.Id).ToList();
                var suitColors = context.SuitColors.ToList();
                context.Heroes.AddRange(
                    new Hero()
                    {
                        Name = "shay",
                        HeroAbility = abilities[0],
                        TrainingStartDate = new DateTime(2020, 5, 4),
                        SuitColors = getRandomSuitColors(suitColors),

                        CurrentPower = 10,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "matan",
                        HeroAbility = abilities[0],
                        TrainingStartDate = new DateTime(2020, 7, 15),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 15,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "hadar",
                        HeroAbility = abilities[0],
                        TrainingStartDate = new DateTime(2020, 9, 22),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 25,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "moti",
                        HeroAbility = abilities[1],
                        TrainingStartDate = new DateTime(2020, 5, 22),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 30,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "gil",
                        HeroAbility = abilities[1],
                        TrainingStartDate = new DateTime(2020, 3, 17),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 45,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "yuval",
                        HeroAbility = abilities[1],
                        TrainingStartDate = new DateTime(2020, 2, 4),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 14,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "alex",
                        HeroAbility = abilities[1],
                        TrainingStartDate = new DateTime(2020, 8, 7),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 20,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "omri",
                        HeroAbility = abilities[0],
                        TrainingStartDate = new DateTime(2020, 6, 2),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 30,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "guy",
                        HeroAbility = abilities[1],
                        TrainingStartDate = new DateTime(2020, 5, 14),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 50,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "ron",
                        HeroAbility = abilities[1],
                        TrainingStartDate = new DateTime(2020, 9, 12),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 60,
                        TrainerId = getRandomElement(usersIds)
                    },
                    new Hero()
                    {
                        Name = "avi",
                        HeroAbility = abilities[0],
                        TrainingStartDate = new DateTime(2020, 10, 18),
                        SuitColors = getRandomSuitColors(suitColors),
                        CurrentPower = 70,
                        TrainerId = getRandomElement(usersIds)
                    }
                    );
                context.SaveChanges();
            }
        }




        private T getRandomElement<T>(IList<T> list)
        {
            return list[new Random().Next(0, list.Count - 1)];
        }


        private IList<SuitColor> getRandomSuitColors(IList<SuitColor> suitColors)
        {
            return (List<SuitColor>)new List<SuitColor>
                            {getRandomElement(suitColors), getRandomElement(suitColors) };
            
        }
    }
}
