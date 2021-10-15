using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebAppIntegrationTest
{
    class JwtMiddlewareIntegrationTest : TestBuilder, IClassFixture<CustomWebApplicationFactory>
    {
        public JwtMiddlewareIntegrationTest(CustomWebApplicationFactory factory)
        {
            ConfigureTest(factory, services => { });
        }
    }
}
