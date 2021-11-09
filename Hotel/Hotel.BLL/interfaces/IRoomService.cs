using Hotel.BLL.DTO;
using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IRoomService
    {
        void Create(RoomDto roomDto);
        Room FindById(int id); 
        bool IsExistById(int id);
        void Update(int roomId, RoomDto roomDto);
        void DeleteById(int id);
    }
}
