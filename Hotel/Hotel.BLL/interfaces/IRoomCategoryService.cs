using Hotel.BLL.DTO;
using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IRoomCategoryService
    {
        void Create(RoomCategoryDto roomCategoryDto);

        RoomCategory FindById(int id);
        bool IsExistById(int id);
        void Update(int categoryId, RoomCategoryDto roomCategoryDto);
        void DeleteById(int id);
    }
}
