using AutoMapper;
using Md_exercise.Core.Domain;
using Md_exercise.Core.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Mapping_profiles
{
    public class HeroMappingProfile : Profile
    {
        public HeroMappingProfile()
        {
            CreateMap<string, Ability>().ConvertUsing(ability => new Ability() {Name=ability });


            CreateMap<IList<SuitColor>, IList<string>>().
                ConvertUsing(suitColors =>
                suitColors.Select((sc)=>sc.Name).
                ToList());

   

            CreateMap<Hero, HeroDto>()
                .ForMember(dest =>
            dest.AbilityName,
            opt => opt.MapFrom(src => src.HeroAbility.Name));





        }
    }
}
