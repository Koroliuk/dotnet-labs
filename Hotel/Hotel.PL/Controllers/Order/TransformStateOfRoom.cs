using System;
using System.Linq;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Order
{
    public class TransformStateOfRoom : Command
    {
        private readonly IOrderService _orderService;

        public TransformStateOfRoom(IUserService userService, IOrderService orderService) :
            base(
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

                Console.Write("Enter an id of order to transform: ");
                var stringOrderId = Console.ReadLine();

                if (stringOrderId != null && !stringOrderId.Equals(string.Empty))
                {
                    var orderId = Convert.ToInt32(stringOrderId);
                    var isOrderExists = _orderService.IsExistsById(orderId);
                    if (!isOrderExists)
                    {
                        throw new HotelException("There is no a such room");
                    }

                    var order = _orderService.FindById(orderId);
                    _orderService.TransformFromBookedToRented(order);
                    var totalPrice = order.End.Subtract(order.Start).Days * order.Room.RoomCategory.PricePerDay;
                    Console.WriteLine("Total price is " + totalPrice);
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
