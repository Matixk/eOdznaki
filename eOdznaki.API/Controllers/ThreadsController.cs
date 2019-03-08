using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.Threads;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Mvc;
using eOdznaki.Models;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadsRepository context;
        private readonly IMapper mapper;

        public ThreadsController(IThreadsRepository context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Threads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThreadPreviewDto>>> GetThreads()
        {
            var threads = await context.GetAllThreads();

            return Ok(mapper.Map<IEnumerable<ThreadPreviewDto>>(threads));
        }

        // GET: api/Threads/5
        [HttpGet("{threadId}")]
        public async Task<ActionResult<Thread>> GetThread(int threadId)
        {
            try
            {
                var thread = await context.GetThread(threadId);

                return Ok(thread);
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

        // PUT: api/Threads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThread(int userId, int id, ThreadForUpdateDto thread)
        {
            try
            {
                await context.Update(userId, id, thread);

                return Ok();
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

        // POST: api/Threads
        [HttpPost]
        public async Task<ActionResult<ThreadPreviewDto>> PostThread(ThreadForCreateDto thread)
        {
            try
            {
                var threadCreated = await context.Insert(thread);

                return CreatedAtRoute("GetThread", new { id = threadCreated.Id }, threadCreated);
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

        // DELETE: api/Threads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Thread>> DeleteThread(int userId, int id)
        {
            try
            {
                return Ok(await context.Delete(userId, id));
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
