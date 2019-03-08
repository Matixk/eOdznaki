using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using eOdznaki.Dtos.Threads;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Repositories
{
    public class ThreadsRepository : IThreadsRepository
    {
        private readonly DataContext context;

        public ThreadsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Thread>> GetAllThreads()
        {
            return await context
                .Threads
                .ToListAsync();
        }

        public async Task<Thread> GetThread(int threadId)
        {
            var thread = await context
                .Threads
                .Include("Posts")
                .FirstOrDefaultAsync(t => t.Id == threadId);
            
            if (thread == null)
            {
                throw new ArgumentNullException(nameof(threadId));
            }

            return thread;
        }

        public async Task<Thread> Insert(ThreadForCreateDto thread)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == thread.AuthorId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(thread.AuthorId));
            }

            var threadToCreate = new Thread(thread.AuthorId, thread.Title, user);

            context.Threads.Add(threadToCreate);
            await context.SaveChangesAsync();

            return threadToCreate;
        }

        public async Task Update(int userId, int threadId, ThreadForUpdateDto thread)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var threadEntity = await context
                .Threads
                .FirstOrDefaultAsync(t => t.Id == threadId);

            if (threadEntity == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            // TODO permission for admin/moderator
            if (user.Id != threadEntity.AuthorId)
            {
                throw new AuthenticationException();
            }

            threadEntity.Title = thread.Title;

            context.Entry(threadEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Thread> Delete(int userId, int threadId)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var threadEntity = await context
                .Threads
                .FirstOrDefaultAsync(t => t.Id == threadId);

            if (threadEntity == null)
            {
                throw new ArgumentNullException(nameof(threadId));
            }

            // TODO permission for admin/moderator
            if (user.Id != threadEntity.AuthorId)
            {
                throw new AuthenticationException();
            }

            context.Remove(threadEntity);
            await context.SaveChangesAsync();

            return threadEntity;
        }

    }
}
