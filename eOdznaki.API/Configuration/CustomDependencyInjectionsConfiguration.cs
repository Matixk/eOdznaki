using eOdznaki.Helpers;
using eOdznaki.Interfaces;
using eOdznaki.Repositories;
using eOdznaki.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eOdznaki.Configuration
{
    public static class CustomDependencyInjectionsConfiguration
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<Seeder>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IForumThreadsRepository, ForumThreadsRepository>();
            services.AddScoped<IForumPostsRepository, ForumPostsRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IAnnouncementsRepository, AnnouncementsRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
        }
    }
}
