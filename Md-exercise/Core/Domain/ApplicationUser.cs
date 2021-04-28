using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public IList<Hero> Heroes { get; set; }
    }
}
