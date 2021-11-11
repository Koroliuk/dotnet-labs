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
                Authorize();

                Console.Write("Enter an id of order to transform: ");
                var stringOrderId = Console.ReadLine();


                var orderId = Convert.ToInt32(stringOrderId);
                var isOrderExists = _orderService.IsExistsById(orderId);
                if (!isOrderExists)
                {
                    throw new HotelException("There is no a such room");
                }

                var totalPrice = _orderService.TransformFromBookedToRentedById(orderId);
                Console.WriteLine("Total price is " + totalPrice);
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