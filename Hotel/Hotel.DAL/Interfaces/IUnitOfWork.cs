using System;
using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<Room> Rooms { get; }
        IRepository<RoomCategory> RoomCategories { get; }
        void Save();
    }
}
