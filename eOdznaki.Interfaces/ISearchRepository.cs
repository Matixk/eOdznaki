using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface ISearchRepository
    {
        Task<PagedList<ForumThread>> SearchForum(SearchParams searchParams);
    }
}