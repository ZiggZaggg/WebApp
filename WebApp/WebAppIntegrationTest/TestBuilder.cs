using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp2.Contexts;
using WebApp2.Models;
using WebApp2.Services.Interfaces;

namespace WebAppIntegrationTest
{
    public class TestBuilder
    {
        protected HttpClient httpClient;
        protected IConfiguration config;

        protected ApplicationContext ConfigureTest(CustomWebApplicationFactory factory, Action<IServiceCollection> action)
        {
            factory.ConfigureTestServices(action);

            var scope = factory.Services.CreateScope();
            httpClient = factory.CreateClient();
            config = scope.ServiceProvider.GetService<IConfiguration>();
            var dbContext = scope.ServiceProvider.GetService<ApplicationContext>();
            var connection = dbContext.Database.GetDbConnection();
            connection.Close();
            connection.Open();
            dbContext.Database.EnsureCreated();

            var token = scope.ServiceProvider.GetService<ITokenService>().GenerateJwtToken(new User { Id = 1 });
            httpClient.DefaultRequestHeaders.Add("AuthorizationToken", token);
            return dbContext;
        }
    }
}
