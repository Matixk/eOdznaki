﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Configuration;
using eOdznaki.Dtos.Badges;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models.Badges;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eOdznaki.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BadgesController : ControllerBase
    {
        private readonly ILogger<BadgesController> logger;
        private readonly IMapper mapper;
        private readonly IBadgeRepository repository;

        public BadgesController(IBadgeRepository repository,
            ILogger<BadgesController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] BadgeParams badgeParams)
        {
            try
            {
                var badges = await repository.GetAllBadges(badgeParams);

                Response.AddPagination(badges.CurrentPage, badges.PageSize, badges.TotalCount, badges.TotalPages);

                return Ok(badges);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all badges: {ex}");
                return BadRequest("Failed to get all badges");
            }
        }

//        TODO Fix InvalidOperationException: The constraint reference 'BadgeTypeEnum' could not be resolved to a type.
//        [HttpGet("{type:BadgeTypeEnum}")]
//        public async Task<IActionResult> GetAsync([FromQuery] BadgeParams badgeParams, BadgeTypeEnum type)
//        {
//            try
//            {
//                var badges = await repository.GetBadgesByType(badgeParams, type);
//
//                Response.AddPagination(badges.CurrentPage, badges.PageSize, badges.TotalCount, badges.TotalPages);
//
//                return Ok(badges);
//            }
//            catch (Exception ex)
//            {
//                logger.LogError($"Failed to get badges of type {type}: {ex}");
//                return BadRequest("Failed to get badges of type {type}");
//            }
//        }

        [HttpPost("/newBadgeTrails")]
        public async Task<IActionResult> PostAsync([FromBody] BadgeTrailsForCreationDto badge)
        {
            logger.LogInformation("Adding new trail badge was called");
            try
            {
                if (ModelState.IsValid)
                {
                    var newBadge = mapper.Map<BadgeTrailsForCreationDto, BadgeTrails>(badge);

                    newBadge.BadgeLevel = 0;
                    newBadge.PointsAquired = 0;
                    newBadge.BadgeStatus = "Inactive";

                    await repository.AddBadge(newBadge);

                    if (await repository.SaveAll())
                        return Created("api/badges", mapper.Map<BadgeTrails, BadgeTrailsForCreationDto>(newBadge));
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add new trails badge: {ex}");
            }

            return BadRequest("Failed to add new trails badge");
        }

        [HttpPost("/newBadgeDrops")]
        public async Task<IActionResult> PostAsync([FromBody] BadgeDropsForCreationDto badge)
        {
            logger.LogInformation("Adding new drops badge was called");
            try
            {
                if (ModelState.IsValid)
                {
                    var newBadge = mapper.Map<BadgeDropsForCreationDto, BadgeDrops>(badge);

                    newBadge.BadgeLevel = 0;
                    newBadge.ReachedHeight = 0;
                    newBadge.BadgeStatus = "Inactive";

                    await repository.AddBadge(newBadge);

                    if (await repository.SaveAll())
                        return Created("api/badges", mapper.Map<BadgeDrops, BadgeDropsForCreationDto>(newBadge));
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add new drops badge: {ex}");
            }

            return BadRequest("Failed to add new drops badge");
        }

        [HttpPost("/newBadgeSummit")]
        public async Task<IActionResult> PostAsync([FromBody] BadgeSummit badge)
        {
            logger.LogInformation("Adding new summit badge was called");
            try
            {
                if (ModelState.IsValid)
                {
                    badge.BadgeStatus = "Inactive";

                    await repository.AddBadge(badge);

                    if (await repository.SaveAll()) return Created("api/badges", badge);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add new summit badge: {ex}");
            }

            return BadRequest("Failed to add new summit badge");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            logger.LogInformation("Delete badge was called");
            if (await repository.DeleteBadgeById(id)) Ok("api/badges");
            logger.LogError($"Failed to delete badge with an id: {id}");
            return BadRequest($"Failed to delete badge with an id: {id}");
        }
    }
}