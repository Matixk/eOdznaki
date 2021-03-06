﻿using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
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

        public async Task<PagedList<ForumPost>> GetForumThreadPosts(int forumThreadId, ForumPostsParams forumPostsParams)
        {
            var posts = context
                .ForumPosts
                .Where(p => p.ForumThreadId == forumThreadId)
                .Include(p => p.Author)
                .AsQueryable();

            if (posts == null) throw new ArgumentNullException(nameof(forumThreadId));

            return await PagedList<ForumPost>.CreateAsync(posts, forumPostsParams.PageNumber,
                forumPostsParams.PageSize);
        }

        public async Task<ForumPost> Insert(ForumPostForCreateDto forumPost)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == forumPost.AuthorId);

            if (user == null) throw new ArgumentNullException(nameof(forumPost.AuthorId));

            var forumThread = await context
                .ForumThreads
                .FirstOrDefaultAsync(f => f.Id == forumPost.ForumThreadId);

            if (forumThread == null) throw new ArgumentNullException(nameof(forumPost.ForumThreadId));

            var forumPostToCreate = new ForumPost(forumPost.AuthorId, forumPost.ForumThreadId, forumPost.Content);

            context.ForumPosts.Add(forumPostToCreate);
            await context.SaveChangesAsync();

            return forumPostToCreate;
        }

        public async Task<ForumPost> Update(int userId, int forumPostId, ForumPostForUpdateDto forumPost, bool sudo)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new ArgumentNullException(nameof(userId));

            var forumPostEntity = await context
                .ForumPosts
                .FirstOrDefaultAsync(f => f.Id == forumPostId);

			if (forumPostEntity == null) throw new ArgumentNullException(nameof(forumPostId));

            if (userId != forumPostEntity.AuthorId && !sudo) throw new AuthenticationException();

            forumPostEntity.Content = forumPost.Content;

            context.Entry(forumPostEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return forumPostEntity;
        }

        public async Task<ForumPost> Delete(int userId, int forumPostId, bool sudo)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new ArgumentNullException(nameof(userId));

            var forumPostEntity = await context
                .ForumPosts
                .FirstOrDefaultAsync(f => f.Id == forumPostId);

            if (forumPostEntity == null) throw new ArgumentNullException(nameof(forumPostId));

            if (userId != forumPostEntity.AuthorId && !sudo) throw new AuthenticationException();

            context.Remove(forumPostEntity);
            await context.SaveChangesAsync();

            return forumPostEntity;
        }
    }
}