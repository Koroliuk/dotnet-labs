using System;
using System.Collections;
using System.Collections.Generic;
using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IOrderService
    {
        void BookRoom(Room room, User user, DateTime startDate, DateTime endDate);
        void RentRoom(Room room, User user, DateTime startDate, DateTime endDate);

        void TransformFromBookedToRented(Order order);
        IEnumerable<Room> GetFreeRooms(DateTime startDate, DateTime endDate);
        Order FindById(int id);
        bool IsExistsById(int id);
    }
}
