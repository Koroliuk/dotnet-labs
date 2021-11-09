using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext _context;

        public RoomRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Rooms;
        }

        public Room Get(int id)
        {
            return _context.Rooms.Find(id);
        }

        public IEnumerable<Room> Find(Func<Room, bool> predicate)
        {
            return _context.Rooms.Where(predicate).ToList();
        }

        public void Create(Room item)
        {
            _context.Rooms.Add(item);
        }

        public void Update(Room item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
        }

        public Room FindById(int id)
        {
            return _context.Rooms.Find(id);
        }
    }
}
