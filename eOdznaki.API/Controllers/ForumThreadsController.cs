using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Mvc;
using eOdznaki.Models;
using Microsoft.AspNetCore.Identity;

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

        // GET: api/ForumThreads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumThreadPreviewDto>>> GetForumThreads()
        {
            var forumThreads = await context.GetAllForumThreads();

            return Ok(mapper.Map<IEnumerable<ForumThreadPreviewDto>>(forumThreads));
        }

        // GET: api/ForumThreads/5
        [HttpGet("{forumThreadId}")]
        public async Task<ActionResult<ForumThreadPreviewDto>> GetForumThread(int forumThreadId)
        {
            try
            {
                var forumThread = await context.GetForumThread(forumThreadId);

                return Ok(mapper.Map<ForumThreadPreviewDto>(forumThread));
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;
                if (paramName != null)
                {
                    return NotFound(paramName);
                }

                throw;
            }
        }

        // GET: api/ForumThreads/text
        [HttpPost("{text}")]
        public async Task<ActionResult<ForumThreadPreviewDto>> FindForumThreads(string text)
        {
            var forumThreads = await context.FindForumThreads(text);

            return Ok(mapper.Map<ForumThreadPreviewDto>(forumThreads));
        }

        // PUT: api/ForumThreads/5
        [HttpPut("{forumThreadId}")]
        public async Task<ActionResult<ForumThreadPreviewDto>> PutForumThread(int forumThreadId, ForumThreadForUpdateDto forumThread)
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

                if (paramName != null)
                {
                    return NotFound(paramName);
                }

                throw;
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        // POST: api/ForumThreads
        [HttpPost]
        public async Task<ActionResult<ForumThreadPreviewDto>> PostForumThread(ForumThreadForCreateDto forumThread)
        {
            try
            {
                var forumThreadCreated = await context.Insert(forumThread);

                return CreatedAtRoute("GetForumThread", new { id = forumThreadCreated.Id }, forumThreadCreated);
            }
            catch (ArgumentNullException e)
            {
                var paramName = e.ParamName;

                if (paramName != null)
                {
                    return NotFound(paramName);
                }

                throw;
            }
        }

        // DELETE: api/ForumThreads/5
        [HttpDelete("{forumThreadId}")]
        public async Task<ActionResult<ForumThreadPreviewDto>> DeleteForumThread(int forumThreadId)
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

                if (paramName != null)
                {
                    return NotFound(paramName);
                }

                throw;
            }
        }

        private async Task<User> GetUser()
        {
            return await userManager.GetUserAsync(HttpContext.User);
        }

    }
}
