using System;
using System.Globalization;
using System.Linq;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Order
{
    public class RentRoom : Command
    {
        private readonly IRoomService _roomService;
        private readonly IOrderService _orderService;

        public RentRoom(IUserService userService, IRoomService roomService, IOrderService orderService) : base(
            userService)
        {
            _roomService = roomService;
            _orderService = orderService;
        }

        public override void Execute()
        {
            try
            {
                var currUser = Authorize();

                if (currUser == null)
                {
                    throw new HotelException("You need to be authorized for that operation");
                }

                Console.Write("Enter a start date: ");
                var stringStartDate = Console.ReadLine();
                Console.Write("Enter an end date: ");
                var stringEndDate = Console.ReadLine();
                Console.Write("Enter an room number(Id): ");
                var stringRoomId = Console.ReadLine();

                if (stringStartDate != null && !stringStartDate.Equals(string.Empty) && stringEndDate != null &&
                    !stringEndDate.Equals(string.Empty) && stringRoomId != null && !stringRoomId.Equals(string.Empty))
                {
                    var roomId = Convert.ToInt32(stringRoomId);
                    var room = _roomService.FindById(roomId);
                    if (room == null)
                    {
                        throw new HotelException("There is no a such room");
                    }

                    var startDate = DateTime.ParseExact(stringStartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(stringEndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    var freeRooms = _orderService.GetFreeRooms(startDate, endDate);

                    if (freeRooms.Contains(room))
                    {
                        _orderService.RentRoom(room, currUser, startDate, endDate);
                        var totalPrice = endDate.Subtract(startDate).Days * room.RoomCategory.PricePerDay;
                        Console.WriteLine("Total price is " + totalPrice);
                    }
                    else
                    {
                        throw new HotelException("A such room is not available at this range of time");
                    }
                }
                else
                {
                    throw new HotelException("Invalid input...");
                }
            }
            catch (HotelException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
