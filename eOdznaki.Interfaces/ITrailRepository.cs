using eOdznaki.Dtos;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models.Trails;
using System.Threading.Tasks;

namespace eOdznaki.Interfaces
{
    public interface ITrailRepository
    {
        Task<PagedList<Trail>> GetAllTrailsAsync(TrailsParams trailsParams);
        Task<Trail> GetTrail(int trailId);
        Task<Trail> Add(TrailDto newTrail);
        Task<Trail> Delete(int trailId);
    }
}
