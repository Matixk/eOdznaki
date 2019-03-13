using Microsoft.Extensions.DependencyInjection;

namespace eOdznaki.Configuration
{
    public static class CustomAuthorizationConfiguration
    {
        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("RequireMemberRole", policy => policy.RequireRole("Admin", "Member"));
            });
        }
    }
}
