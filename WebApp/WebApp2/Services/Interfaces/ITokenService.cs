using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Models;

namespace WebApp2.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        long GetUserId(string token);
    }
}
