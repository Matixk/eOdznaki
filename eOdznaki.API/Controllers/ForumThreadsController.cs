using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Mvc;
using eOdznaki.Models;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumThreadsController : ControllerBase
    {
        private readonly IForumThreadsRepository context;
        private readonly IMapper mapper;

        public ForumThreadsController(IForumThreadsRepository context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
        public async Task<ActionResult<ForumThread>> GetForumThread(int forumThreadId)
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

        // PUT: api/ForumThreads/5
        [HttpPut("{forumThreadId}")]
        public async Task<IActionResult> PutForumThread(int userId, int forumThreadId, ForumThreadForUpdateDto forumThread)
        {
            try
            {
                var forumThreadUpdated = await context.Update(userId, forumThreadId, forumThread);

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
        public async Task<ActionResult<ForumThread>> DeleteForumThread(int userId, int forumThreadId)
        {
            try
            {
                return Ok(await context.Delete(userId, forumThreadId));
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

    }
}
