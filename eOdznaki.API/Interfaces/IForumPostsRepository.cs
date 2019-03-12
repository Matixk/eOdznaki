﻿using System.Threading.Tasks;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IForumPostsRepository
    {
        Task<ForumPost> Insert(ForumPostForCreateDto forumPost);
        Task<ForumPost> Update(int userId, int forumPostId, ForumPostForUpdateDto forumPost);
        Task<ForumPost> Delete(int userId, int forumPostId);
    }
}