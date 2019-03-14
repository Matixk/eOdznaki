using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
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

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = context.Users.Include("UserForumThreads").Include("UserForumPosts").AsQueryable();

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
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