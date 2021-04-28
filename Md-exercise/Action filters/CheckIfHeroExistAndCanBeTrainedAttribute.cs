using Md_exercise.Core.Dto_s;
using Md_exercise.Core.Services.DbChecker;
using Md_exercise.Persistence;
using Md_exercise.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Md_exercise.Action_filters
{
    public class CheckIfHeroExistAndCanBeTrainedAttribute : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context,

                ActionExecutionDelegate next)
        {
            var dbCheckerService = (DbChecker)context.HttpContext.RequestServices.
            GetService(typeof(IDbChecker));
            var heroDto = (HeroDto)context.ActionArguments["heroDto"];

            if (heroDto.Id == null)
            {
                context.Result = new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Error = new ResponseError { Messages = new[] { "hero id was not specified  " } }
                });
                return;
            }
           
            if (!await dbCheckerService.IsHeroExistAsync(heroDto.Id.Value))
            {

                context.Result = new NotFoundObjectResult(new Response { IsSuccess = false, 
                    Error = new ResponseError {Messages= new[] { "there is no such Hero" } } }) ;
                return;
            }


            if (!await dbCheckerService.IsTrainerHasThisHero(
                context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                heroDto.Id.Value))
            {

                context.Result = new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Error = new ResponseError { Messages = new[] { "this hero belongs to another trainer, so you can't train him" } }
                });
                return;
            }


            if (await dbCheckerService.IsHeroAlreadyTrainedFiveTimesTodayAsync(heroDto.Id.Value))
            {
                context.Result = new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Error = new ResponseError { Messages = new[] { "this hero has already trained 5 times today" } }
                });
                return;
            }
       
            await next();
            
        }


    }
}
