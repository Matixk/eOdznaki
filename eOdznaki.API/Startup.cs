using AutoMapper;
using eOdznaki.Configuration;
using eOdznaki.Helpers;
using eOdznaki.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace eOdznaki
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.IncludeIgnoredWarning)));
            services.AddCustomIdentity();
            services.AddCustomAuthentication(Configuration);
            services.AddCustomAuthorization();
            services.AddCustomMvc();
            services.BuildServiceProvider().GetService<DataContext>().Database.Migrate();
            services.AddCors();
            services.AddDependencyInjections();
            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("eOdznaki", new Info {Title = "eOdznakiPrototype", Version = "v1"});
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seeder seeder)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            seeder.SeedRoles();
            seeder.SeedAdmin();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/eOdznaki/swagger.json", "eOdznaki"); });
            app.UseMvc();
            app.UseHttpsRedirection();
        }
    }
}