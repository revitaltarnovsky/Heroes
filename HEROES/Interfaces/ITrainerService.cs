using HEROES.DTOs;
using HEROES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Interfaces
{
    public interface ITrainerService
    {
        Task<Trainer> GetTrainerById(long Id);
        Task<Trainer> GetTrainerByUsername(string username);
        Task<bool> TrainerExists(string username);
        Task<TrainerMemberDto> GetTrainerWithHeroes(long id);
    }
}
