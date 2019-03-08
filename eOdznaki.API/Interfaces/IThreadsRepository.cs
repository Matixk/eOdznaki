using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.Threads;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IThreadsRepository
    {
        Task<IEnumerable<Thread>> GetAllThreads();
        Task<Thread> GetThread(int threadId);
        Task<Thread> Insert(ThreadForCreateDto thread);
        Task Update(int userId, int id, ThreadForUpdateDto thread);
        Task<Thread> Delete(int userId, int id);
    }
}
