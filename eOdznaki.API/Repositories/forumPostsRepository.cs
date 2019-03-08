using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Repositories
{
    public class ForumPostsRepository : IForumPostsRepository
    {
        private readonly DataContext context;

        public ForumPostsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ForumPost>> GetAllForumPosts()
        {
            return await context
                .ForumPosts
                .ToListAsync();
        }

        public async Task<ForumPost> GetForumPosts(int forumPostsId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ForumPost> Insert(ForumPostForCreateDto forumPost)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ForumPost> Update(int userId, int forumPostId, ForumPostForUpdateDto forumPost)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ForumPost> Delete(int userId, int forumPostId)
        {
            throw new System.NotImplementedException();
        }
    }
}
