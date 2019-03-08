using System.Threading.Tasks;
using eOdznaki.Models;

namespace eOdznaki.Persistence.Interfaces
{
    public interface IUsersRepository
    {
//        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUsers();
        Task<User> GetUser(int id);
    }
}