using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp2.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration config;
        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            this.next = next;
            this.config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["AuthorizationToken"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await next(context);
        }

        public void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(config["TokenKey"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                context.Items["UserId"] = userId;
            }
            catch
            {
            }
        }
    }
}
