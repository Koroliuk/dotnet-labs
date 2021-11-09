using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;

namespace Hotel.PL.Controllers.User
{
    public class SignUp : ICommand
    {
        private readonly IUserService _userService;
        public SignUp(IUserService userService)
        {
            _userService = userService;
        }

        public void Execute()
        {
            Console.Write("Enter a login: ");
            var login = Console.ReadLine();
            Console.Write("Enter a password: ");
            var password = Console.ReadLine();
            if (login != null && !login.Equals(string.Empty) && password != null && !password.Equals(string.Empty))
            {
                var userDto = new UserDto {Login = login, PasswordHash = password};
                _userService.Save(userDto);
            }
            else
            {
                Console.WriteLine("An error occurred");
            }
        }
    }
}