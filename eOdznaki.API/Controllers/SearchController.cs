using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Helpers.Params;
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

        [HttpPost]
        public async Task<IActionResult> SearchForum(SearchParams searchParams)
        {
            var found = await context.SearchForum(searchParams.Regex);

            return Ok(mapper.Map<Dictionary<ForumThreadPreviewDto, IEnumerable<ForumPostPreviewDto>>>(found));
        }
    }
}