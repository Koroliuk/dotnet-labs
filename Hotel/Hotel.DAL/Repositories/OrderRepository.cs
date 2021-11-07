using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly HotelContext _context;

        public OrderRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        public Order Get(int id)
        {
            return _context.Orders.Find(id);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return _context.Orders.Where(predicate).ToList();
        }

        public void Create(Order item)
        {
            _context.Orders.Add(item);
        }

        public void Update(Order item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }
    }
}