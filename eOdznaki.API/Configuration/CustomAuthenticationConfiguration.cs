using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace eOdznaki.Configuration
{
    public static class CustomAuthenticationConfiguration
    {
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
    }
}
