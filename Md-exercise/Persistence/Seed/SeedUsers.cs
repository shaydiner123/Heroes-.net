using Md_exercise.Core.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Persistence.Seed
{
    public class SeedUsers
    {
        public async Task SeedUsersData(HeroesDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                List<string> names = new List<string> { "Shay", "Hadar", "Matan", "Guy", "Omri" };
                string password = "12345678!C";
                List<string> emails = new List<string> { $"{names[0]}12321@hotmail.com" ,
                    $"{names[1]}12321@hotmail.com" ,
                    $"{names[2]}12321@hotmail.com" ,
                    $"{names[3]}12321@hotmail.com" ,
                    $"{names[4]}12321@hotmail.com"
                };
                int index = 0;
                foreach (string name in names)
                {
                    var user = new ApplicationUser { UserName = name, Email = emails[index] };
                    var result = await userManager.CreateAsync(user, password);
                    index++;
                }
            }
        }
    }
}
