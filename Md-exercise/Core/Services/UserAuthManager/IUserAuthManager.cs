using Md_exercise.Core.Dto_s;
using Md_exercise.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Services.UserAuthManager
{
        public interface IUserAuthManager
        {
            Task<Result<LogInResult>> SignUpAsync(ApplicationUserDto userDto);
            Task<Result<LogInResult>> LogInAsync(ApplicationUserDto userDto);
          
        }
}
