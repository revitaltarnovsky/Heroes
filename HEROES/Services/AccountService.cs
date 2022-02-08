using AutoMapper;
using HEROES.Data;
using HEROES.DTOs;
using HEROES.Interfaces;
using HEROES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HEROES.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TrainerDto> Register(AuthDto authDto)
        {

            using var hmac = new HMACSHA512();

            var trainer = new Trainer
            {
                UserName = authDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(authDto.Password)),
                PasswordSalt = hmac.Key
            };


            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            return new TrainerDto
            {
                Token = _tokenService.CreateToken(trainer),
            };
        }
    }
}
