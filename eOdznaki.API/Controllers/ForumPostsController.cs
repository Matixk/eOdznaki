using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Configuration;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{forumThreadId}")]
        public async Task<IActionResult> GetThreadPosts(int forumThreadId, [FromQuery] ForumPostsParams forumPostsParams)
        {
            try
            {
                var posts = await context.GetForumThreadPosts(forumThreadId, forumPostsParams);

                Response.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount,
                    posts.TotalPages);

                return Ok(mapper.Map<IEnumerable<ForumPostPreviewDto>>(posts));
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;
                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpPut("{forumPostId}")]
        public async Task<IActionResult> PutForumPost(int forumPostId,
            ForumPostForUpdateDto forumPost)
        {
            try
            {
                var user = await GetUser();
                var forumPostUpdated = await context.Update(user.Id, forumPostId, forumPost, IsSudo());

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

        [Authorize(Policy = "RequireMemberRole")]
        [HttpPost]
        public async Task<IActionResult> PostForumPost(ForumPostForCreateDto forumPost)
        {
            try
            {
                var user = await GetUser();

                forumPost.AuthorId = user.Id;

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

        [Authorize(Policy = "RequireMemberRole")]
        [HttpDelete("{forumPostId}")]
        public async Task<IActionResult> DeleteForumPost(int forumPostId)
        {
            try
            {
                var user = await GetUser();
                var forumPostDeleted = await context.Delete(user.Id, forumPostId, IsSudo());

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

        private bool IsSudo()
        {
            return User.IsInRole("Admin") || User.IsInRole("Moderator");
        }
    }
}