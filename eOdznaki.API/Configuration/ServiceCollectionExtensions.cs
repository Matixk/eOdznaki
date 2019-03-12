using System.Text;
using eOdznaki.Helpers;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using eOdznaki.Repositories;
using eOdznaki.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace eOdznaki.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
        }

        public static void AddCustomAuthentication(this IServiceCollection services, string token)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(token)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("RequireMemberRole", policy => policy.RequireRole("Admin", "Member"));
            });
        }

        public static void AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

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