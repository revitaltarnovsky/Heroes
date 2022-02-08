using HEROES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Interfaces
{
    public interface IAccountService
    {
        Task<TrainerDto> Register(AuthDto authDto);
    }
}
