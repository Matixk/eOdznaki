using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.Announcements;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    interface IAnnouncementsRepository
    {
        Task<PagedList<Announcement>> GetCurrentAnnouncements(AnnouncementsParams announcementsParams);
        Task<Announcement> Insert(AnnouncementForCreateDto announcement);
        Task<Announcement> Update(int announcementId, AnnouncementForUpdateDto announcement);
        Task<Announcement> Delete(int announcementId);
    }
}
