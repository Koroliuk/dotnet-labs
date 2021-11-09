using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.User
{
    public class SignUp : Command
    {
        private readonly IUserService _userService;

        public SignUp(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        public override void Execute()
        {
            Console.Write("Enter a login: ");
            var login = Console.ReadLine();
            Console.Write("Enter a password: ");
            var password = Console.ReadLine();
            Console.Write("Enter an user role (User or Admin): ");
            var role = Console.ReadLine();

            if (login != null && !login.Equals(string.Empty) &&
                password != null && !password.Equals(string.Empty) &&
                role != null && !role.Equals(string.Empty))
            {
                var userDto = new UserDto
                {
                    Login = login,
                    PasswordHash = password,
                    Role = role
                };

                try
                {
                    _userService.SignUp(userDto);
                    Console.WriteLine("User was successful registered!");
                }
                catch (HotelException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid input...");
            }
        }
    }
}
