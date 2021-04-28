using Md_exercise.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Models
{
    public class LogInResult
    {
        public Token Token { get; set; }

        public ApplicationUser User { get; set; }
    }
}
