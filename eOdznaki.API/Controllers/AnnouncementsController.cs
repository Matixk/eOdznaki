using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.Announcements;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Mvc;
using eOdznaki.Models;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/Announcements
        [HttpGet]
        public async Task<ActionResult<PagedList<AnnouncementPreviewDto>>> GetAnnouncements([FromQuery] AnnouncementsParams forumPostsParams)
        {
            var announcements = await context.GetCurrentAnnouncements(forumPostsParams);

            Response.AddPagination(announcements.CurrentPage, announcements.PageSize, announcements.TotalCount, announcements.TotalPages);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcements));
        }

        // PUT: api/Announcements/5
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<IActionResult> PutAnnouncement(int announcementId, AnnouncementForUpdateDto announcement)
        {
            var announcementUpdated = await context.Update(announcementId, announcement);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcementUpdated));
        }

        // POST: api/Announcements
        [HttpPost]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<ActionResult<Announcement>> PostAnnouncement(AnnouncementForCreateDto announcement)
        {
            var announcementCreated = await context.Insert(announcement);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcementCreated));
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<ActionResult<Announcement>> DeleteAnnouncement(int id)
        {
            var announcementDeleted = await context.Delete(id);

            return Ok(mapper.Map<AnnouncementPreviewDto>(announcementDeleted));
        }

    }
}
