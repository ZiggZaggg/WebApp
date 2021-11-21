using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp2.Contexts;
using WebApp2.Models;
using WebApp2.Models.DTOs;
using WebApp2.Services.Interfaces;

namespace WebApp2.Services
{
    public class UserService : IUserService
    {
        private ApplicationContext context;

        public UserService(ApplicationContext context)
        {
            this.context = context;
        }

        public User AddUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            if (context.Users.Any(u => u.Email.Equals(email)))
            {
                return null;
            }

            // ADD PASS HASH
            var user = context.Users.Add(new User(email, password)).Entity;
            context.SaveChanges();

            return user;
        }

        public User FindByEmail(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User FindById(long id)
        {
            return context.Users.SingleOrDefault(u => u.Id == id);
        }

        public bool VerifyPassword(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var user = FindByEmail(email);

            if (user == null)
            {
                return false;
            }

            return user.Password == password ? true : false;
        }

        private bool ValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public bool ValidPassword(string password)
        {
            return !(string.IsNullOrEmpty(password) || password.Length < 8);
        }

        public string Register(UserRequestDTO userRequestDTO)
        {
            if (!ValidEmail(userRequestDTO.Email) || !ValidPassword(userRequestDTO.Password))
            {
                return "Invalid email or password.";
            }

            if (FindByEmail(userRequestDTO.Email) != null)
            {
                return "Email already taken.";
            }

            AddUser(userRequestDTO.Email, userRequestDTO.Password);

            return "Ok";
        }

        public string Login(UserRequestDTO userRequestDTO)
        {
            if (!ValidEmail(userRequestDTO.Email) || !ValidPassword(userRequestDTO.Password))
            {
                return "Invalid email or password.";
            }

            if (FindByEmail(userRequestDTO.Email) == null)
            {
                return "Email not found.";
            }

            if (!VerifyPassword(userRequestDTO.Email, userRequestDTO.Password))
            {
                return "Invalid password.";
            }

            return "Ok";
        }
    }
}
