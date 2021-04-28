using AutoMapper;
using Md_exercise.Core;
using Md_exercise.Core.Domain;
using Md_exercise.Core.Dto_s;
using Md_exercise.Core.Services.UserAuthManager;
using Md_exercise.Log4net;
using Md_exercise.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private readonly IMapper mapper;
        private IUserAuthManager userAuthManager;
        private IConfiguration configuration;
        private IUnitOfWork unitOfWork;
   
        public AccountsController(IUserAuthManager userAuthManager, IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper/*, Utiles utiles*/)
        {
            this.userAuthManager = userAuthManager;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(ApplicationUserDto userDto)
        {
            try
            {
                Result<LogInResult> res = await userAuthManager.SignUpAsync(userDto);
                if (res.IsSuccess)
                {

                    userDto = this.mapper.Map<ApplicationUser, ApplicationUserDto>(res.Content.User);

                    return Created(res.Content.User.Id, new Response
                    {
                        IsSuccess = true,
                        Content = new { Token = res.Content.Token, User = userDto }
                    });
                }
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { IsSuccess = false, Error = new ResponseError { Messages = res.ErrorMessages } });
            }
            catch (Exception e)
            {
                log.Error("something went wrong with signup process ", e);
                return this.Genarete500Response(e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(ApplicationUserDto userDto)
        {
            try
            {
                Result<LogInResult> res = await userAuthManager.LogInAsync(userDto);
                if (res.IsSuccess)
                {
                    userDto = this.mapper.Map<ApplicationUser, ApplicationUserDto>(res.Content.User);
                    return Ok(new Response
                    {
                        IsSuccess = true,
                        Content = new { Token = res.Content.Token, User = userDto }
                    });
                }

                return Unauthorized(new Response { IsSuccess = false, Error = new ResponseError { Messages = res.ErrorMessages } });
            }
            catch (Exception e)
            {
                log.Error("something went wrong with login process ", e);
                return this.Genarete500Response(e);
            }
        }

    }
}
