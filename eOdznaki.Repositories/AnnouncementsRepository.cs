using System;
using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Dtos.Announcements;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly DataContext context;

        public AnnouncementsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<PagedList<Announcement>> GetCurrentAnnouncements(AnnouncementsParams announcementsParams)
        {
            var currentAnnouncements = context
                .Announcements
                .Where(e => e.Expiration >= DateTime.Now)
                .AsQueryable();

            return await PagedList<Announcement>.CreateAsync(currentAnnouncements, announcementsParams.PageNumber,
                announcementsParams.PageSize);
        }

        public async Task<Announcement> Insert(AnnouncementForCreateDto announcement)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == announcement.AuthorId);

            if (user == null) throw new ArgumentNullException(nameof(announcement.AuthorId));

            var announcementToCreate =
                new Announcement(
                    announcement.Title,
                    announcement.Content,
                    announcement.AuthorId,
                    announcement.Expiration,
                    user
                );

            context.Announcements.Add(announcementToCreate);
            await context.SaveChangesAsync();

            return announcementToCreate;
        }

        public async Task<Announcement> Update(int announcementId, AnnouncementForUpdateDto announcement)
        {
            var announcementEntity = await context
                .Announcements
                .FirstOrDefaultAsync(e => e.Id == announcementId);

            if (announcement == null) throw new ArgumentNullException(nameof(announcementId));

            announcementEntity.Title = announcement.Title;
            announcementEntity.Content = announcement.Content;
            announcementEntity.Expiration = announcement.Expiration;

            context.Entry(announcementEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return announcementEntity;
        }

        public async Task<Announcement> Delete(int announcementId)
        {
            var announcement = await context
                .Announcements
                .FirstOrDefaultAsync(e => e.Id == announcementId);

            if (announcement == null) throw new ArgumentNullException(nameof(announcementId));

            context.Remove(announcement);
            await context.SaveChangesAsync();

            return announcement;
        }
    }
}