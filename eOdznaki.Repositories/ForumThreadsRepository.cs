﻿using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Repositories
{
    public class ForumThreadsRepository : IForumThreadsRepository
    {
        private readonly DataContext context;

        public ForumThreadsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<PagedList<ForumThread>> GetAllForumThreads(ForumThreadsParams forumThreadsParams)
        {
            var forumThreads = context
                .ForumThreads
                .Include(t => t.Author)
                .AsQueryable();

            return await PagedList<ForumThread>.CreateAsync(forumThreads, forumThreadsParams.PageNumber,
                forumThreadsParams.PageSize);
        }

        public async Task<ForumThread> GetForumThread(int forumThreadId)
        {
            var forumThread = await context
                .ForumThreads
                .FirstOrDefaultAsync(t => t.Id == forumThreadId);

            if (forumThread == null) throw new ArgumentNullException(nameof(forumThreadId));

            return forumThread;
        }

        public async Task<ForumThread> Insert(ForumThreadForCreateDto forumThread)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == forumThread.AuthorId);

            if (user == null) throw new ArgumentNullException(nameof(forumThread.AuthorId));

            var threadToCreate = new ForumThread(forumThread.AuthorId, forumThread.Title);

            context.ForumThreads.Add(threadToCreate);
            var postToCreate = new ForumPost(user.Id, threadToCreate.Id, forumThread.Content);

            context.ForumPosts.Add(postToCreate);
            await context.SaveChangesAsync();

            return threadToCreate;
        }

        public async Task<ForumThread> Update(int userId, int forumThreadId, ForumThreadForUpdateDto forumThread, bool sudo)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new ArgumentNullException(nameof(userId));

            var forumThreadEntity = await context
                .ForumThreads
                .FirstOrDefaultAsync(t => t.Id == forumThreadId);

            if (forumThreadEntity == null) throw new ArgumentNullException(nameof(forumThreadId));

            if (user.Id != forumThreadEntity.AuthorId && !sudo) throw new AuthenticationException();

            forumThreadEntity.Title = forumThread.Title;

            context.Entry(forumThreadEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return forumThreadEntity;
        }

        public async Task<ForumThread> Delete(int userId, int forumThreadId, bool sudo)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new ArgumentNullException(nameof(userId));

            var forumThreadEntity = await context
                .ForumThreads
                .FirstOrDefaultAsync(t => t.Id == forumThreadId);

            if (forumThreadEntity == null) throw new ArgumentNullException(nameof(forumThreadId));

            if (user.Id != forumThreadEntity.AuthorId && !sudo) throw new AuthenticationException();

            context.Remove(forumThreadEntity);
            await context.SaveChangesAsync();

            return forumThreadEntity;
        }
    }
}