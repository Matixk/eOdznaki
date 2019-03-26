using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace eOdznaki.Configuration
{
    public static class CustomAuthenticationConfiguration
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var token = configuration.GetSection("Secrets:FacebookAppId").Value;
            var fbId = configuration.GetSection("Secrets:FacebookAppId").Value;
            var fbSecret = configuration.GetSection("Secrets:FacebookAppKey").Value;


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
                })
                .AddFacebook(options =>
                {
                    options.AppId = fbId;
                    options.AppSecret = fbSecret;
                });

        }
    }
}
