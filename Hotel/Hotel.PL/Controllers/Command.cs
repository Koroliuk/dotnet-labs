using System;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;

namespace Hotel.PL.Controllers
{
    public abstract class Command
    {
        private readonly IUserService _userService;

        protected Command(IUserService userService)
        {
            _userService = userService;
        }

        public abstract void Execute();

        protected void Authorize()
        {
            Console.Write("Enter a login: ");
            var login = Console.ReadLine();
            Console.Write("Enter a password: ");
            var password = Console.ReadLine();
            if (login != null && !login.Equals(string.Empty) &&
                password != null && !password.Equals(string.Empty))
            {
                var user = _userService.FindByLogin(login);
                if (user == null)
                {
                    throw new HotelException("Invalid credentials");
                }
            }
            else
            {
                throw new HotelException("Invalid credentials");
            }
        }

        protected void AuthorizeAsAdmin()
        {
            Console.Write("Enter a login: ");
            var login = Console.ReadLine();
            Console.Write("Enter a password: ");
            var password = Console.ReadLine();
            if (login != null && !login.Equals(string.Empty) &&
                password != null && !password.Equals(string.Empty))
            {
                var user = _userService.FindByLogin(login);
                if (user == null)
                {
                    throw new HotelException("Invalid credentials");
                }

                if (user.Role != UserRole.Admin)
                {
                    throw new HotelException("Access permitted");
                }
            }
            else
            {
                throw new HotelException("Invalid credentials");
            }
        }
    }
}
