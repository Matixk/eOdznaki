using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models.Trails;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eOdznaki.Repositories
{
    class TrailRepository : ITrailRepository
    {
        private readonly DataContext context;

        public TrailRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Trail> Add(Trail trail)
        {
            context.Add(trail);
            await context.SaveChangesAsync();

            return trail;
        }

        public async Task<Trail> Delete(int trailId)
        {
            var trail = await context.Trails.FirstOrDefaultAsync(t => t.Id == trailId);
            context.Remove(trail);
            await context.SaveChangesAsync();

            return trail;
        }

        public async Task<PagedList<Trail>> GetAllTrailsAsync(TrailsParams trailsParams)
        {
            var trails = context.Trails.AsQueryable();

            return await PagedList<Trail>.CreateAsync(trails, trailsParams.PageNumber, trailsParams.PageSize);
        }

        public async Task<Trail> GetTrail(int trailId)
        {
            return await context.Trails.FirstOrDefaultAsync(t => t.Id == trailId);
        }
    }
}
