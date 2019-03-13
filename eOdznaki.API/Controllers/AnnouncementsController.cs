using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Configuration;
using eOdznaki.Dtos.Announcements;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementsRepository context;
        private readonly IMapper mapper;

        public AnnouncementsController(IAnnouncementsRepository context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements(
            [FromQuery] AnnouncementsParams forumPostsParams)
        {
            var announcements = await context.GetCurrentAnnouncements(forumPostsParams);

            Response.AddPagination(announcements.CurrentPage, announcements.PageSize, announcements.TotalCount,
                announcements.TotalPages);

            return Ok(mapper.Map<IEnumerable<AnnouncementPreviewDto>>(announcements));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<IActionResult> PutAnnouncement(int announcementId, AnnouncementForUpdateDto announcement)
        {
            var announcementUpdated = await context.Update(announcementId, announcement);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcementUpdated));
        }

        [HttpPost]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<IActionResult> PostAnnouncement(AnnouncementForCreateDto announcement)
        {
            var announcementCreated = await context.Insert(announcement);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcementCreated));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcementDeleted = await context.Delete(id);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcementDeleted));
        }
    }
}