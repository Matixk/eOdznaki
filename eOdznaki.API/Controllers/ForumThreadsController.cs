using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Configuration;
using eOdznaki.Dtos.ForumThreads;
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
    public class ForumThreadsController : ControllerBase
    {
        private readonly IForumThreadsRepository context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ForumThreadsController(IForumThreadsRepository context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetForumThreads([FromQuery] ForumThreadsParams forumThreadsParams)
        {
            var forumThreads = await context.GetAllForumThreads(forumThreadsParams);

            Response.AddPagination(forumThreads.CurrentPage, forumThreads.PageSize, forumThreads.TotalCount,
                forumThreads.TotalPages);

            return Ok(mapper.Map<IEnumerable<ForumThreadPreviewDto>>(forumThreads));
        }

        [HttpGet("{forumThreadId}")]
        public async Task<IActionResult> GetForumThread(int forumThreadId)
        {
            try
            {
                var forumThread = await context.GetForumThread(forumThreadId);

                return Ok(mapper.Map<ForumThreadPreviewDto>(forumThread));
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;
                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpPut("{forumThreadId}")]
        public async Task<IActionResult> PutForumThread(int forumThreadId, ForumThreadForUpdateDto forumThread)
        {
            try
            {
                var user = await GetUser();

                var forumThreadUpdated = await context.Update(user.Id, forumThreadId, forumThread);
                return Ok(mapper.Map<ForumThreadPreviewDto>(forumThreadUpdated));
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
        public async Task<IActionResult> PostForumThread(ForumThreadForCreateDto forumThread)
        {
            try
            {
                var forumThreadCreated = await context.Insert(forumThread);

                return CreatedAtRoute("GetForumThread", new {id = forumThreadCreated.Id}, forumThreadCreated);
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null) return NotFound(paramName);

                throw;
            }
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpDelete("{forumThreadId}")]
        public async Task<IActionResult> DeleteForumThread(int forumThreadId)
        {
            try
            {
                var user = await GetUser();
                var forumThreadDeleted = await context.Delete(user.Id, forumThreadId);

                return Ok(mapper.Map<ForumThreadPreviewDto>(forumThreadDeleted));
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