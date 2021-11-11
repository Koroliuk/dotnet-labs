using System;
using System.Globalization;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Order
{
    public class BookRoom : Command
    {
        private readonly IOrderService _orderService;

        public BookRoom(IUserService userService, IOrderService orderService) : base(
            userService)
        {
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

                var roomId = Convert.ToInt32(stringRoomId);

                var startDate = DateTime.ParseExact(stringStartDate ?? throw new HotelException("Invalid input"),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(stringEndDate ?? throw new InvalidOperationException("Invalid input"),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture);

                _orderService.BookRoomById(roomId, currUser, startDate, endDate);
            }
            catch (HotelException e)
            {
                Console.WriteLine(e.Message);
            }
            catch
            {
                Console.WriteLine("Invalid input");
            }
        }
    }
}
