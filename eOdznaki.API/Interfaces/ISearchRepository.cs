using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface ISearchRepository
    {
        Task<Dictionary<ForumThread, IEnumerable<ForumPost>>> SearchForum(string regex);
    }
}
