using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp2.Contexts;
using WebApp2.Helpers;
using WebApp2.Services;
using WebApp2.Services.Interfaces;

namespace WebAppIntegrationTest
{
    public class TestStartup
    {
        protected readonly IConfiguration configuration;
        public TestStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.configuration.Bind(new ConfigurationBuilder().AddUserSecrets("2b450229-593e-4df0-923b-fe95840b119b"));
        }


        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            var inMemorySqlite = new SqliteConnection("Datasource=:memory:");
            inMemorySqlite.Open();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(inMemorySqlite));
        }
    }
}
