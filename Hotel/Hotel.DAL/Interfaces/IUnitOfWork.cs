using System;
using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        IRoomRepository Rooms { get; }
        IRoomCategoryRepository RoomCategories { get; }
        void Save();
    }
}
