using eOdznaki.Dtos;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models.Locations;
using eOdznaki.Models.Trails;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOdznaki.Repositories
{
    public class TrailRepository : ITrailRepository
    {
        private readonly DataContext context;

        public TrailRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Trail> Add(TrailDto newTrail)
        {
            var startPoint = new Location(newTrail.StartPoint.Longitude, newTrail.StartPoint.Latitude);
            var endPoint = new Location(newTrail.EndPoint.Longitude, newTrail.EndPoint.Latitude);
            var newStartPoint =  context.Add(startPoint);
            var newEndPoint =  context.Add(endPoint);

            IEnumerable<Location> checkpoints = new List<Location>();

            if (newTrail.Checkpoints != null)
            {
                foreach (var item in newTrail.Checkpoints)
                {
                    checkpoints.Append(item);
                }
            }
            var newCheckPoint = context.Add(checkpoints);
            var trail = new Trail(newStartPoint.Entity, newEndPoint.Entity, newCheckPoint.Entity);
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
            return await context.Trails.Include(e => e.StartPoint).Include(e => e.EndPoint).Include(e => e.Checkpoints).FirstOrDefaultAsync(t => t.Id == trailId);
        }
    }
}
