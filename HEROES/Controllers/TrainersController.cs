using HEROES.Data;
using HEROES.DTOs;
using HEROES.Extensions;
using HEROES.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Controllers
{
    [Authorize]
    public class TrainersController : BaseApiController
    {
        private readonly ITrainerService _trainerService;
        private readonly ILogger<TrainersController> _logger;


        public TrainersController(ITrainerService trainerService, ILogger<TrainersController> logger)
        {
            _trainerService = trainerService;
            _logger = logger;
        }

        /// <summary>
        /// Get trainer with his heroes list
        /// </summary>
        /// <returns><see cref="TrainerMemberDto"/></returns>
        [HttpGet("trainer")]
        public async Task<ActionResult<TrainerMemberDto>> GetTrainerwithHeroes()
        {
            var trainerId = User.GetTrainerId();

            _logger.LogInformation("Trainer with id {trainerId} available to watch", trainerId);

            return Ok(await _trainerService.GetTrainerWithHeroes(trainerId));
        }

    }
}
