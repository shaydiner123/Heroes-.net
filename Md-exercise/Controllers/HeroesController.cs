using AutoMapper;
using Md_exercise.Action_filters;
using Md_exercise.Core;
using Md_exercise.Core.Domain;
using Md_exercise.Core.Dto_s;
using Md_exercise.Log4net;
using Md_exercise.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Md_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HeroesController : ControllerBase
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private readonly IMapper mapper;
        private IUnitOfWork unitOfWork;



        public HeroesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet("{heroId?}")]
        public async Task<IActionResult> GetHeroes([FromRoute] Guid? heroId)
        {
            try
            {
                var trainerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var heroes = await this.unitOfWork.HeroRepo.GetHeroesByAsync(heroId, trainerId);
                IEnumerable<HeroDto> heroesDtos = heroes.Select(mapper.Map<Hero, HeroDto>);
                return Ok(new Response { IsSuccess = true, Content = heroesDtos });
            }
            catch (Exception e)
            {
                log.Error(e);
                return this.Genarete500Response(e);
            }
        }




        [HttpPatch("trainHero")]
        [CheckIfHeroExistAndCanBeTrained]
        public async Task<IActionResult> TrainHero([FromBody] HeroDto heroDto)
        {
            try
            {  
                Hero hero = await this.unitOfWork.HeroRepo.TrainHeroAsync(heroDto.Id.Value);
                await this.unitOfWork.SaveAsync();
                heroDto = mapper.Map<Hero, HeroDto>(hero);
                return Ok(new Response
                {
                    IsSuccess = true,
                    Content = heroDto
                });
            }
            catch (Exception e)
            {
                log.Error("The  hero has not been updated, see more details in the error description ", e);
                return this.Genarete500Response(e);
            }
        }
         
        
    }
}
