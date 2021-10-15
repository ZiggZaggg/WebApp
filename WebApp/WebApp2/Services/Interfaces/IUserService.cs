using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Models;
using WebApp2.Models.DTOs;

namespace WebApp2.Services.Interfaces
{
    public interface IUserService
    {
        User AddUser(string email, string password);
        User FindByEmail(string email);
        bool ValidPassword(string password);
        string Register(UserRequestDTO userRequestDTO);
        string Login(UserRequestDTO userRequestDTO);
        User FindById(long id);
    }
}
