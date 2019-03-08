using AutoMapper;
using eOdznaki.Models.Badges;
using eOdznaki.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOdznaki.Controllers
{
    public class BadgesController : ControllerBase
    {
        private readonly IBadgeRepository repository;
        private readonly ILogger<BadgesController> logger;
        private readonly IMapper mapper;

        public BadgesController(IBadgeRepository repository,
            ILogger<BadgesController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var badges = await repository.GetAllBadges();
                return Ok(badges);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all badges: {ex}");
                return BadRequest("Failed to get all badges");
            }
        }

        [HttpGet("type:BadgeTypeEnum")]
        public async Task<IActionResult> GetAsync(BadgeTypeEnum type)
        {
            try
            {
                var badges = await repository.GetBadgesByType(type);
                return Ok(badges);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get badges of type {type}: {ex}");
                return BadRequest("Failed to get badges of type {type}");
            }
        }
    }
}
