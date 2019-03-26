using AutoMapper;
using eOdznaki.Configuration;
using eOdznaki.Dtos;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace eOdznaki.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly ILogger<TrailsController> logger;
        private readonly IMapper mapper;
        private readonly ITrailRepository repository;

        public TrailsController(ITrailRepository repository,
            ILogger<TrailsController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrails([FromQuery] TrailsParams trailsParams)
        {
            var trails = await repository.GetAllTrailsAsync(trailsParams);

            Response.AddPagination(trails.CurrentPage, trails.PageSize, trails.TotalCount,
                trails.TotalPages);

            return Ok(trails);
        }

        [HttpGet("{trailId}")]
        public async Task<IActionResult> GetTrail(int trailId)
        {
            try
            {
                var trail = await repository.GetTrail(trailId);

                return Ok(trail);
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;
                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostTrail(TrailDto trail)
        {
            try
            {
                var newTrail = await repository.Add(trail);

                return Ok();
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        [HttpDelete("{trailId}")]
        public async Task<IActionResult> DeleteTrail(int trailId)
        {
            try
            {
                var deletedTrail = await repository.Delete(trailId);

                return Ok(deletedTrail);
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }
    }
}
