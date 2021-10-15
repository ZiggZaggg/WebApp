using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp2.Helpers;
using WebApp2.Models;
using WebApp2.Services;
using Xunit;

namespace WebAppIntegrationTest
{
    public class JwtMiddlewareIntegrationTest : TestBuilder, IClassFixture<CustomWebApplicationFactory>
    {
        public JwtMiddlewareIntegrationTest(CustomWebApplicationFactory factory)
        {
            ConfigureTest(factory, services => { });
        }

        [Fact]
        public void AttachUserToContext_OK()
        {
            var httpContextMoq = new Mock<HttpContext>();
            httpContextMoq.Setup(c => c.Items).Returns(new Dictionary<object, object>());
            var nextMoq = new Mock<RequestDelegate>();
            var configMoq = new Mock<IConfiguration>();
            configMoq.Setup(c => c.GetValue(typeof(string), "TokenKey")).Returns(config["TokenKey"]);
            var tokenService = new TokenService(config);
            var token = tokenService.GenerateJwtToken(new User { Id = 1 });
            var jwtMiddleware = new JwtMiddleware(nextMoq.Object, configMoq.Object);

            jwtMiddleware.AttachUserToContext(httpContextMoq.Object, token);


        }
    }
}
