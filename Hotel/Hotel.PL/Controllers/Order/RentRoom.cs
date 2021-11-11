using System;
using System.Globalization;
using System.Linq;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Order
{
    public class RentRoom : Command
    {
        private readonly IOrderService _orderService;

        public RentRoom(IUserService userService, IOrderService orderService) : base(
            userService)
        {
            _orderService = orderService;
        }

        public override void Execute()
        {
            try
            {
                var currUser = Authorize();

                Console.Write("Enter a start date: ");
                var stringStartDate = Console.ReadLine();
                Console.Write("Enter an end date: ");
                var stringEndDate = Console.ReadLine();
                Console.Write("Enter an room number(Id): ");
                var stringRoomId = Console.ReadLine();

                var roomId = Convert.ToInt32(stringRoomId);

                var startDate = DateTime.ParseExact(stringStartDate ?? throw new HotelException("Invalid input"),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(stringEndDate ?? throw new HotelException("Invalid input"),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var totalPrice = _orderService.RentRoomById(roomId, currUser, startDate, endDate);
                Console.WriteLine("Total price is " + totalPrice);
            }
            catch (HotelException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
