using HEROES.Data;
using HEROES.DTOs;
using HEROES.Interfaces;
using HEROES.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HEROES.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly ITrainerService _trainerService;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITrainerService trainerService, ILogger<AccountController> logger, ITokenService tokenService)
        {
            _accountService = accountService;
            _trainerService = trainerService;
            _logger = logger;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Register trainer
        /// </summary>
        /// <param name="authDto">The model to register trainer</param>
        /// <returns>Returns <see cref="TrainerDto"/></returns>
        [HttpPost("register")]
        public async Task<ActionResult<TrainerDto>> Register(AuthDto authDto)
        {
            if (await _trainerService.TrainerExists(authDto.Username)) return BadRequest("Username is taken");

            _logger.LogInformation("A trainer with username {username} registered to the hero company at {registerTime}.", authDto.Username, DateTime.Now);

            return Ok(await _accountService.Register(authDto));
        }

        /// <summary>
        /// Login trainer
        /// </summary>
        /// <param name="authDto">The model to login trainer</param>
        /// <returns>Returns <see cref="TrainerDto"/></returns>
        [HttpPost("login")]
        public async Task<ActionResult<TrainerDto>> Login(AuthDto authDto)
        {
            var trainer = await _trainerService.GetTrainerByUsername(authDto.Username);

            if (trainer == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(trainer.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(authDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != trainer.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            _logger.LogInformation("A trainer with username {username} logged in at {loginTime}.", trainer.UserName, DateTime.Now);

            return Ok(new TrainerDto
            {
                Token = _tokenService.CreateToken(trainer),
            });
        }

    }
}
