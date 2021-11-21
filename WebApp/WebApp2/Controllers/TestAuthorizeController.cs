using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp2.Helpers;
using WebApp2.Models.DTOs;
using WebApp2.Services.Interfaces;

namespace WebApp2.Controllers
{
    [Authorize]
    public class TestAuthorizeController : MasterController
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public TestAuthorizeController(ITokenService tokenService, IHttpContextAccessor httpContext, IUserService userService) : base (httpContext)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }

        [HttpGet("/testAuth")]
        public IActionResult TestAuth()
        {
            var user = userService.FindById(userId);
            return Ok(new UserResponseDTO(user));
        }
    }
}
