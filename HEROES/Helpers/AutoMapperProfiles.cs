using AutoMapper;
using HEROES.DTOs;
using HEROES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Trainer, TrainerDto>();
            CreateMap<Trainer, TrainerMemberDto>();
            CreateMap<Hero, HeroDto>();
        }
    }
}
