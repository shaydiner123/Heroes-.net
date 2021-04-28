using Md_exercise.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence.Seed
{
    public class SeedSuitColors
    {
        public void SeedSuitColorsData(HeroesDbContext context)
        {
            if (!context.SuitColors.Any())
            {
                System.Drawing.Color MyColor;
                foreach (string ColorName in Enum.GetNames(typeof(System.Drawing.KnownColor)))
                {
                    MyColor = System.Drawing.Color.FromName(ColorName);
                    if (!MyColor.IsSystemColor)
                        context.SuitColors.Add(new SuitColor { Name = ColorName });
                }
                context.SaveChanges();
            }
        }
    }
}
