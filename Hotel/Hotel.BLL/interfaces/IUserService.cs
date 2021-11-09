using System.Net;
using Hotel.BLL.DTO;

namespace Hotel.BLL.interfaces
{
    public interface IUserService
    {
        void Save(UserDto userDto);
    }
}