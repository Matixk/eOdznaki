using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository context;
        private readonly IMapper mapper;

        public SearchController(ISearchRepository context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Search/text
        [HttpPost("{text}")]
        public async Task<ActionResult<Dictionary<ForumThreadPreviewDto, IEnumerable<ForumPostPreviewDto>>>> SearchForum(string text)
        {
            var found = await context.SearchForum(text);

            return Ok(mapper.Map<Dictionary<ForumThreadPreviewDto, IEnumerable<ForumPostPreviewDto>>>(found));
        }

    }
}