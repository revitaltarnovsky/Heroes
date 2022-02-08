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
    public class TrainerService : ITrainerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrainerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Trainer> GetTrainerById(long Id)
        {
            return await _context.Trainers.FindAsync(Id);
        }

        public async Task<Trainer> GetTrainerByUsername(string username)
        {
            return await _context.Trainers
                .SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task<bool> TrainerExists(string username)
        {
            return await _context.Trainers.AnyAsync(x => x.UserName == username.ToLower());
        }

        public async Task<TrainerMemberDto> GetTrainerWithHeroes(long id)
        {
            var trainer = await _context.Trainers
                .Include(h => h.Heroes.OrderBy(h => h.CurrentPower))
                .SingleOrDefaultAsync(t => t.Id == id);

            return _mapper.Map<TrainerMemberDto>(trainer);
        }
    }
}
