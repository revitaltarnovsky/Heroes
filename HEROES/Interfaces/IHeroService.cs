using HEROES.DTOs;
using HEROES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Interfaces
{
    public interface IHeroService
    {
        Task<HeroDto> AddHero(long trainerId, HeroDto heroDto);
        Task<Hero> GetHero(Guid id);
        Task<HeroDto> GetHeroModel(Guid id);
        Task<IEnumerable<HeroDto>> GetHeroes();
        Task<bool> TrainHero(Guid heroId);
    }
}
