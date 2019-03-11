using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext context;

        public UsersRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = context.Users.Include("UserForumThreads").Include("UserForumPosts").AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            var query = context.Users.Include("UserForumThreads").Include("UserForumPosts").AsQueryable();

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public void Delete(User user)
        {
            context.Remove(user);
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}