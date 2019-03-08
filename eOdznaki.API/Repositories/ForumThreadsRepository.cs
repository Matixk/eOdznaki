using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumThreads;
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

        public async Task<IEnumerable<ForumThread>> GetAllForumThreads()
        {
            return await context
                .ForumThreads
                .ToListAsync();
        }

        public async Task<ForumThread> GetForumThread(int forumThreadId)
        {
            var forumThread = await context
                .ForumThreads
                .Include("Posts")
                .FirstOrDefaultAsync(t => t.Id == forumThreadId);
            
            if (forumThread == null)
            {
                throw new ArgumentNullException(nameof(forumThreadId));
            }

            return forumThread;
        }

        public async Task<IEnumerable<ForumThread>> FindForumThreads(string regex)
        {
            return await context
                .ForumThreads
                .Where(t => t.Title.Contains(regex))
                .ToListAsync();
        }

        public async Task<ForumThread> Insert(ForumThreadForCreateDto forumThread)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == forumThread.AuthorId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(forumThread.AuthorId));
            }

            var forumThreadToCreate = new ForumThread(forumThread.AuthorId, forumThread.Title, user);

            context.ForumThreads.Add(forumThreadToCreate);
            await context.SaveChangesAsync();

            return forumThreadToCreate;
        }

        public async Task<ForumThread> Update(int userId, int forumThreadId, ForumThreadForUpdateDto forumThread)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var forumThreadEntity = await context
                .ForumThreads
                .FirstOrDefaultAsync(t => t.Id == forumThreadId);

            if (forumThreadEntity == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            // TODO permission for admin/moderator
            if (user.Id != forumThreadEntity.AuthorId)
            {
                throw new AuthenticationException();
            }

            forumThreadEntity.Title = forumThread.Title;

            context.Entry(forumThreadEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return forumThreadEntity;
        }

        public async Task<ForumThread> Delete(int userId, int forumThreadId)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var forumThreadEntity = await context
                .ForumThreads
                .FirstOrDefaultAsync(t => t.Id == forumThreadId);

            if (forumThreadEntity == null)
            {
                throw new ArgumentNullException(nameof(forumThreadId));
            }

            // TODO permission for admin/moderator
            if (user.Id != forumThreadEntity.AuthorId)
            {
                throw new AuthenticationException();
            }

            context.Remove(forumThreadEntity);
            await context.SaveChangesAsync();

            return forumThreadEntity;
        }

    }
}
