using System.Threading.Tasks;
using eOdznaki.Configuration;
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

        public SearchController(ISearchRepository context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SearchForum([FromQuery] SearchParams searchParams)
        {
            var foundItems = await context.SearchForum(searchParams);

            Response.AddPagination(foundItems.CurrentPage, foundItems.PageSize, foundItems.TotalCount,
                foundItems.TotalPages);

            return Ok(foundItems);
        }
    }
}