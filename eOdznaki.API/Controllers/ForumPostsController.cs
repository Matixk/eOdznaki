using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumPostsController : ControllerBase
    {
        private readonly IForumPostsRepository context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ForumPostsController(IForumPostsRepository context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: api/ForumPosts/text
        [HttpPost("{text}")]
        public async Task<ActionResult<ForumPostPreviewDto>> FindForumPosts(
            [FromQuery] ForumPostsParams forumPostsParams)
        {
            var forumPosts = await context.FindForumPosts(forumPostsParams);

            Response.AddPagination(forumPosts.CurrentPage, forumPosts.PageSize, forumPosts.TotalCount,
                forumPosts.TotalPages);

            return Ok(mapper.Map<ForumPostPreviewDto>(forumPosts));
        }

        // PUT: api/ForumPosts/5
        [HttpPut("{forumPostId}")]
        public async Task<ActionResult<ForumPostPreviewDto>> PutForumPost(int forumPostId,
            ForumPostForUpdateDto forumPost)
        {
            try
            {
                var user = await GetUser();
                var forumPostUpdated = await context.Update(user.Id, forumPostId, forumPost);

                return Ok(mapper.Map<ForumPostPreviewDto>(forumPostUpdated));
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null) return NotFound(paramName);

                throw;
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        // POST: api/ForumPosts
        [HttpPost]
        public async Task<ActionResult<ForumPostPreviewDto>> PostForumPost(ForumPostForCreateDto forumPost)
        {
            try
            {
                var forumPostCreated = await context.Insert(forumPost);

                return Ok(mapper.Map<ForumPostPreviewDto>(forumPostCreated));
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        // DELETE: api/ForumPost/5
        [HttpDelete("{forumPostId}")]
        public async Task<ActionResult<ForumPostPreviewDto>> DeleteForumPost(int forumPostId)
        {
            try
            {
                var user = await GetUser();
                var forumPostDeleted = await context.Delete(user.Id, forumPostId);

                return Ok(forumPostDeleted);
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        private async Task<User> GetUser()
        {
            return await userManager.GetUserAsync(HttpContext.User);
        }
    }
}