using System;
using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Models;
using eOdznaki.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext context;

        public UsersRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            var query = context.Users.Include("UserThreads").Include("UserPosts").AsQueryable();

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}