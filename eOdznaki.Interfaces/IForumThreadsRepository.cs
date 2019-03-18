using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IForumThreadsRepository
    {
        Task<PagedList<ForumThread>> GetAllForumThreads(ForumThreadsParams forumThreadsParams);
        Task<IEnumerable<ForumPost>> GetForumThread(int forumThreadId);
        Task<ForumThread> Insert(ForumThreadForCreateDto forumThread);
        Task<ForumThread> Update(int userId, int forumThreadId, ForumThreadForUpdateDto forumThread);
        Task<ForumThread> Delete(int userId, int forumThreadId);
    }
}