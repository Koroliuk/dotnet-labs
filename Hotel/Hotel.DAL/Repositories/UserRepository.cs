using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelContext _context;

        public UserRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public User FindByLogin(string login)
        {
            return _context.Users.Find(login);
        }
    }
}
