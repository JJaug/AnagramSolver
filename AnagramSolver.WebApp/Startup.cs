using AnagramSolver.BusinessLogic.Classes.CacheAnagrams;
using AnagramSolver.BusinessLogic.Classes.PersistentRepositories;
using AnagramSolver.BusinessLogic.Classes.Repositories;
using AnagramSolver.BusinessLogic.Classes.SearchLogs;
using AnagramSolver.BusinessLogic.Classes.Services;
using AnagramSolver.BusinessLogic.Classes.Users;
using AnagramSolver.BusinessLogic.Classes.WordRepositories;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.WebApp.Helpers;
using AnagramSolver.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnagramSolver.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("MyConfig:ConnectionString");
            services.AddDbContext<VocabularyDBContext>(options =>
            options.UseSqlServer(connectionString));
            services.AddScoped<ICacheServices, CacheServices>();
            services.AddScoped<ISearchLogServices, SearchLogServices>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IWordServices, WordServices>();
            services.AddScoped<IFileToDatabaseService, FileToDatabaseServices>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISearchLogRepository, SerachLogRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<IFileToDatabaseRepository, FileToDatabaseRepository>();

            services.AddControllersWithViews();
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserLoginService, UserLoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
