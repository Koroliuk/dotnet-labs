using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SignUp(UserDto userDto)
        {
            if (userDto.Login == null || userDto.Login.Equals(string.Empty) ||
                userDto.PasswordHash == null || userDto.PasswordHash.Equals(string.Empty) ||
                userDto.Role == null || userDto.Role.Equals(string.Empty))
            {
                throw new HotelException("Invalid input");
            }
            if (userDto.Role is not ("Admin" or "User"))
            {
                throw new HotelException("Invalid user role");
            }

            if (IsExistByLogin(userDto.Login))
            {
                throw new HotelException("User with a such login is already exists");
            }

            var user = new User
            {
                Login = userDto.Login,
                PasswordHash = userDto.PasswordHash,
                Role = userDto.Role == "Admin" ? UserRole.Admin : UserRole.User
            };

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
        }

        public User FindByLogin(string login)
        {
            return _unitOfWork.Users.FindByLogin(login);
        }

        public bool IsExistByLogin(string login)
        {
            return FindByLogin(login) != null;
        }
    }
}
