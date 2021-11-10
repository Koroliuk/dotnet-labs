using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.BLL.interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void BookRoom(Room room, User user, DateTime startDate, DateTime endDate)
        {
            var order = new Order()
            {
                Start = startDate,
                End = endDate,
                Type = OrderType.Booked,
                User = user,
                Room = room
            };

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();
        }

        public void RentRoom(Room room, User user, DateTime startDate, DateTime endDate)
        {
            var order = new Order()
            {
                Start = startDate,
                End = endDate,
                Type = OrderType.Paid,
                User = user,
                Room = room
            };

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();
        }

        public void TransformFromBookedToRented(Order order)
        {
            order.Type = OrderType.Paid;
            
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }

        public IEnumerable<Room> GetFreeRooms(DateTime startDate, DateTime endDate)
        {
            var occupiedRooms = _unitOfWork.Orders.Find(o => o.Start >= startDate && o.End <= endDate)
                .Select(o => o.Room)
                .Distinct();

            return _unitOfWork.Rooms.GetAll().Where(r => !occupiedRooms.Contains(r));
        }

        public Order FindById(int id)
        {
            return _unitOfWork.Orders.FindById(id);
        }

        public bool IsExistsById(int id)
        {
            return FindById(id) != null;
        }
    }
}