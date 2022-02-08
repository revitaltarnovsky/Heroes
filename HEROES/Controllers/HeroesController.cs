using HEROES.DTOs;
using HEROES.Extensions;
using HEROES.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Controllers
{
    [Authorize]
    public class HeroesController : BaseApiController
    {
        private readonly ILogger<HeroesController> _logger;
        private readonly IHeroService _heroService;

        public HeroesController(ILogger<HeroesController> logger, IHeroService heroService)
        {
            _logger = logger;
            _heroService = heroService;
        }

        /// <summary>
        /// Get Heroes
        /// </summary>
        /// <returns>list of <see cref="HeroDto"/></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroDto>>> GetHeroes()
        {
            _logger.LogInformation("Heroes from heroes company available to watch");
            return Ok(await _heroService.GetHeroes());
        }

        /// <summary>
        /// Get hero by his id
        /// </summary>
        /// <param name="id">The hero id</param>
        /// <returns><see cref="HeroDto"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroDto>> GetHero(Guid id)
        {
            var hero = await _heroService.GetHero(id);

            if (hero == null) return NotFound();

            _logger.LogInformation("Hero named {heroName} available to watch", hero.Name);

            return Ok(await _heroService.GetHeroModel(id));
        }

        /// <summary>
        /// Add hero
        /// </summary>
        /// <param name="heroDto">The model to create hero</param>
        /// <returns><see cref="HeroDto"/></returns>
        [HttpPost(Name = "AddHero")]
        public async Task<ActionResult<HeroDto>> AddHero(HeroDto heroDto)
        {
            var trainerId = User.GetTrainerId();

            var hero = await _heroService.AddHero(trainerId, heroDto);

            _logger.LogInformation("Hero named {heroName} added to trainer {trainerId}.", hero.Name, trainerId);

            return CreatedAtRoute("AddHero", hero);

        }

        /// <summary>
        /// Train hero
        /// </summary>
        /// <param name="id">The hero id</param>
        /// <returns>returns <response code="200"> if hero was trained</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpPatch("{id}")]
        public async Task<ActionResult> TrainHero(Guid id)
        {
            var hero = await _heroService.GetHero(id);

            if (hero == null) return NotFound();

            var isTrained = await _heroService.TrainHero(id);

            if (isTrained == false)
            {
                _logger.LogWarning("Hero cannot train more than 5 times a day");

                return NoContent();
            }

            _logger.LogInformation("Trainer with Id {trainerId} trained hero named {heroName}", User.GetTrainerId(), hero.Name);

            return Ok();

        }
    }
}
