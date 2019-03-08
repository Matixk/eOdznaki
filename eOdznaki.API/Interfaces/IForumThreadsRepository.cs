using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IForumThreadsRepository
    {
        Task<IEnumerable<ForumThread>> GetAllForumThreads();
        Task<ForumThread> GetForumThread(int forumThreadId);
        Task<ForumThread> Insert(ForumThreadForCreateDto forumThread);
        Task<ForumThread> Update(int userId, int forumThreadId, ForumThreadForUpdateDto forumThread);
        Task<ForumThread> Delete(int userId, int forumThreadId);
    }
}
