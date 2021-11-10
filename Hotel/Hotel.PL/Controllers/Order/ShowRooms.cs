using System;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Order
{
    public class ShowRooms : Command
    {
        private readonly IOrderService _orderService;

        public ShowRooms(IUserService userService, IOrderService orderService) : base(
            userService)
        {
            _orderService = orderService;
        }

        public override void Execute()
        {
            try
            {
                Console.Write("Enter a start date: ");
                var stringStartDate = Console.ReadLine();
                Console.Write("Enter an end date: ");
                var stringEndDate = Console.ReadLine();

                if (stringStartDate != null && !stringStartDate.Equals(string.Empty) && stringEndDate != null &&
                    !stringEndDate.Equals(string.Empty))
                {
                    var startDate = DateTime.Parse(stringStartDate);
                    var endDate = DateTime.Parse(stringEndDate);

                    var freeRooms = _orderService.GetFreeRooms(startDate, endDate);

                    foreach (var room in freeRooms)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Room id: {0}", room.Id);
                        Console.WriteLine("Room category name: {0}", room.RoomCategory.Name);
                        Console.WriteLine("Room category price per day: {0}", room.RoomCategory.PricePerDay);
                        Console.WriteLine("Room category capacity: {0}", room.RoomCategory.Capacity);
                        Console.WriteLine("Room category description: {0}", room.RoomCategory.Description);
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
