using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Room FindById(int id);
    }
}