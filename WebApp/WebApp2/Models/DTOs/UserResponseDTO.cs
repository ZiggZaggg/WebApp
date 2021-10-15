using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models.DTOs
{
    public class UserResponseDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserResponseDTO()
        {
        }

        public UserResponseDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.Password;
        }
    }
}
