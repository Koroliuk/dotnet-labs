using Hotel.BLL.DTO;
using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IUserService
    {
        void SignUp(UserDto userDto);
        User FindByLogin(string login);

        bool IsExistByLogin(string login);
    }
}
