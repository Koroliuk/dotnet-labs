using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
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

        public void BookRoomById(int roomId, User user, DateTime startDate, DateTime endDate)
        {
            if (startDate <= DateTime.Now || startDate > endDate)
            {
                throw new HotelException("Invalid input");
            }

            var room = _unitOfWork.Rooms.FindById(roomId);
            if (room == null)
            {
                throw new HotelException("There is no a such room");
            }

            var freeRooms = GetFreeRooms(startDate, endDate);

            if (!freeRooms.Contains(room))
            {
                throw new HotelException("A such room is not available at this range of time");
            }

            var order = new Order
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

        public decimal RentRoomById(int roomId, User user, DateTime startDate, DateTime endDate)
        {
            if (startDate <= DateTime.Now || startDate > endDate)
            {
                throw new HotelException("Invalid input");
            }

            var room = _unitOfWork.Rooms.FindById(roomId);
            if (room == null)
            {
                throw new HotelException("There is no a such room");
            }

            var freeRooms = GetFreeRooms(startDate, endDate);

            if (!freeRooms.Contains(room))
            {
                throw new HotelException("A such room is not available at this range of time");
            }

            var order = new Order
            {
                Start = startDate,
                End = endDate,
                Type = OrderType.Paid,
                User = user,
                Room = room
            };

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();
            
            if (order.Room.RoomCategory != null)
            {
                return order.End.Subtract(order.Start).Days * order.Room.RoomCategory.PricePerDay;
            }

            return default;
        }

        public decimal TransformFromBookedToRentedById(int id)
        {
            var order = FindById(id);

            if (order == null)
            {
                throw new HotelException("There is no such order");
            }

            order.Type = OrderType.Paid;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();

            if (order.Room.RoomCategory != null)
            {
                return order.End.Subtract(order.Start).Days * order.Room.RoomCategory.PricePerDay;
            }

            return default;
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