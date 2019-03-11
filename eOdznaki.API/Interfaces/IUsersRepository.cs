using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        void Delete(User user);
        Task<bool> SaveAll();
    }
}