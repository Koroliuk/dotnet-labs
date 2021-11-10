using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order FindById(int id);
    }
}