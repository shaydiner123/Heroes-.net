using Md_exercise.Core.Domain;
using Md_exercise.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Services.TokenGenerator
{
    public interface ITokenGenerator
    {
        Token GenerateToken(ApplicationUser user);
    }
}
