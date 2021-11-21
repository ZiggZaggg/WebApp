using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Models.DTOs;
using WebApp2.Services.Interfaces;

namespace WebApp2.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost("/register")]
        public IActionResult Register([FromBody] UserRequestDTO userRequestDTO)
        {
            var status = userService.Register(userRequestDTO);

            if (!status.Equals("Ok"))
            {
                return BadRequest(new ErrorDTO(status));
            }

            return Ok(new UserResponseDTO(userService.FindByEmail(userRequestDTO.Email)));
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserRequestDTO userRequestDTO)
        {
            var status = userService.Login(userRequestDTO);

            if (!status.Equals("Ok"))
            {
                return BadRequest(new ErrorDTO(status));
            }

            var user = userService.FindByEmail(userRequestDTO.Email);
            return Ok(new { Token = tokenService.GenerateJwtToken(user) });
        }
    }
}
