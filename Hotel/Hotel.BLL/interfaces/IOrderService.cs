using System;
using System.Collections;
using System.Collections.Generic;
using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IOrderService
    {
        void BookRoomById(int roomId, User user, DateTime startDate, DateTime endDate);
        decimal RentRoomById(int roomId, User user, DateTime startDate, DateTime endDate);

        decimal TransformFromBookedToRentedById(int orderId);
        IEnumerable<Room> GetFreeRooms(DateTime startDate, DateTime endDate);
        Order FindById(int id);
        bool IsExistsById(int id);
    }
}
