using AutoMapper;
using AutoMapper.QueryableExtensions;
using HEROES.Data;
using HEROES.DTOs;
using HEROES.Interfaces;
using HEROES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Services
{
    public class HeroService : IHeroService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HeroService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HeroDto> AddHero(long trainerId, HeroDto heroDto)
        {
            var hero = new Hero
            {
                Name = heroDto.Name.ToLower(),
                Ability = heroDto.Ability,
                SuitColors = heroDto.SuitColors,
                TrainerId = trainerId
            };

            _context.Heroes.Add(hero);

            var trainer = await _context.Trainers.FindAsync(trainerId);

            trainer.Heroes.Add(hero);

            await _context.SaveChangesAsync();

            return _mapper.Map<HeroDto>(hero);
        }

        public async Task<Hero> GetHero(Guid id)
        {
            return await _context.Heroes.FindAsync(id);
        }


        public async Task<HeroDto> GetHeroModel(Guid id)
        {
            return await _context.Heroes.Where(h => h.Id == id)
                .ProjectTo<HeroDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<HeroDto>> GetHeroes()
        {
            return await _context.Heroes
                .ProjectTo<HeroDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> TrainHero(Guid heroId)
        {
            var trainingCount = await _context.Trainings.Where(train => train.HeroId == heroId && train.Date == DateTime.Today).CountAsync();

            if (trainingCount > 4) return false;

            var hero = await _context.Heroes.FindAsync(heroId);

            var rnd = new Random();
            var number = rnd.Next(0, 11);

            hero.CurrentPower += number;

            var training = new Training
            {
                HeroId = hero.Id,
                Date = DateTime.Today
            };

            await _context.Trainings.AddAsync(training);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
