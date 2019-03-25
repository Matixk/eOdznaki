using System.Threading.Tasks;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IForumPostsRepository
    {
        Task<ForumPost> Insert(ForumPostForCreateDto forumPost);
        Task<PagedList<ForumPost>> GetForumThreadPosts(int forumThreadId, ForumPostsParams forumPostsParams);
        Task<ForumPost> Update(int userId, int forumPostId, ForumPostForUpdateDto forumPost, bool sudo);
        Task<ForumPost> Delete(int userId, int forumPostId, bool sudo);
    }
}