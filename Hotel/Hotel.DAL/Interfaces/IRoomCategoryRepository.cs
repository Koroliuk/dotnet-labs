using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IRoomCategoryRepository : IRepository<RoomCategory>
    {
        RoomCategory FindById(int id);
    }
}