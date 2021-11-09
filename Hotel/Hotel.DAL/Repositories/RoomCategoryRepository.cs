using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.Repositories
{
    namespace Hotel.DAL.Repositories
    {
        public class RoomCategoryRepository : IRoomCategoryRepository
        {
            private readonly HotelContext _context;

            public RoomCategoryRepository(HotelContext context)
            {
                _context = context;
            }

            public IEnumerable<RoomCategory> GetAll()
            {
                return _context.RoomCategories;
            }

            public RoomCategory Get(int id)
            {
                return _context.RoomCategories.Find(id);
            }

            public IEnumerable<RoomCategory> Find(Func<RoomCategory, bool> predicate)
            {
                return _context.RoomCategories.Where(predicate).ToList();
            }

            public void Create(RoomCategory item)
            {
                _context.RoomCategories.Add(item);
            }

            public void Update(RoomCategory item)
            {
                _context.Entry(item).State = EntityState.Modified;
            }

            public void Delete(int id)
            {
                var roomCategory = _context.RoomCategories.Find(id);
                if (roomCategory != null)
                {
                    _context.RoomCategories.Remove(roomCategory);
                }
            }

            public RoomCategory FindById(int id)
            {
                return _context.RoomCategories.Find(id);
            }
        }
    }
}
