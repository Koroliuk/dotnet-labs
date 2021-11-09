using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
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

        public void Save(UserDto userDto)
        {
            var user = new User {Login = userDto.Login, PasswordHash = userDto.PasswordHash};
            _unitOfWork.Users.Create(user);
        }
    }
}