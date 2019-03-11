using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IForumPostsRepository
    {
        Task<PagedList<ForumPost>> FindForumPosts(ForumPostsParams forumPostsParams);
        Task<ForumPost> Insert(ForumPostForCreateDto forumPost);
        Task<ForumPost> Update(int userId, int forumPostId, ForumPostForUpdateDto forumPost);
        Task<ForumPost> Delete(int userId, int forumPostId);
    }
}
