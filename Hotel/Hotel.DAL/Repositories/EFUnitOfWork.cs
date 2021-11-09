using System;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Repositories.Hotel.DAL.Repositories;

namespace Hotel.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly HotelContext _context;
        private UserRepository _userRepository;
        private RoomRepository _roomRepository;
        private RoomCategoryRepository _roomCategoryRepository;
        private OrderRepository _orderRepository;

        public EFUnitOfWork()
        {
            _context = new HotelContext();
        }

        public IUserRepository Users
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }

        public IRepository<Order> Orders
        {
            get { return _orderRepository ??= new OrderRepository(_context); }
        }

        public IRepository<Room> Rooms
        {
            get { return _roomRepository ??= new RoomRepository(_context); }
        }

        public IRoomCategoryRepository RoomCategories
        {
            get { return _roomCategoryRepository ??= new RoomCategoryRepository(_context); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
