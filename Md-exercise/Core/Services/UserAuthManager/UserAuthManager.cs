using Md_exercise.Core.Domain;
using Md_exercise.Core.Dto_s;
using Md_exercise.Core.Services.TokenGenerator;
using Md_exercise.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Services.UserAuthManager
{

    public class UserAuthManager : IUserAuthManager
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ITokenGenerator tokenGenerator;


        public UserAuthManager(UserManager<ApplicationUser> usm, SignInManager<ApplicationUser> signIM, ITokenGenerator tokenGenerator)
        {
            this.userManager = usm;
            this.signInManager = signIM;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<Result<LogInResult>> SignUpAsync(ApplicationUserDto userDto)
        {
            ApplicationUser user = new ApplicationUser { UserName = userDto.UserName, Email = userDto.Email };
            IdentityResult result = await this.userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                Token token = this.tokenGenerator.GenerateToken(user);
                return new Result<LogInResult>
                {
                    IsSuccess = true,
                    Content = new LogInResult { Token = token, User = user }
                };
            }
            IEnumerable<string> errors = result.Errors.Select(e => e.Description);
            return new Result<LogInResult>
            {
                IsSuccess = false,
                ErrorMessages = errors
            };
        }

        public async Task<Result<LogInResult>> LogInAsync(ApplicationUserDto userDto)
        {
            var user = await userManager.FindByNameAsync(userDto.UserName);
            if (user == null)
            {
                return new Result<LogInResult>
                {
                    IsSuccess = false,
                    ErrorMessages = new[] { "The username is not exist" }
                };
            }
            bool isCorrectPassword = await userManager.CheckPasswordAsync(user, userDto.Password);
            if (!isCorrectPassword)
            {
                return new Result<LogInResult>
                {
                    IsSuccess = false,
                    ErrorMessages = new[] { "The password you entered is incorrect, please try again" }
                };

            }
            Token token = this.tokenGenerator.GenerateToken(user);
            return new Result<LogInResult>
            {
                IsSuccess = true,
                Content = new LogInResult { Token = token, User = user }
            };
        }

    }
}
