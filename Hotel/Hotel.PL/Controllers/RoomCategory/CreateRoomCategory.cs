using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.RoomCategory
{
    public class CreateRoomCategory : Command

    {
        private readonly IRoomCategoryService _roomCategoryService;

        public CreateRoomCategory(IRoomCategoryService roomCategoryService, IUserService userService) : base(
            userService)
        {
            _roomCategoryService = roomCategoryService;
        }

        public override void Execute()
        {
            try
            {
                AuthorizeAsAdmin();

                Console.Write("Enter a category name: ");
                var name = Console.ReadLine();
                Console.Write("Enter a price per day: ");
                var pricePerDayString = Console.ReadLine();
                Console.Write("Enter a capacity: ");
                var capacityString = Console.ReadLine();
                Console.Write("Enter a description: ");
                var description = Console.ReadLine();
                
                var pricePerDay = Convert.ToDecimal(pricePerDayString);
                var capacity = Convert.ToInt32(capacityString);
                
                var roomCategoryDto = new RoomCategoryDto()
                {
                    Name = name,
                    PricePerDay = pricePerDay,
                    Capacity = capacity,
                    Description = description
                };

                _roomCategoryService.Create(roomCategoryDto);
            }
            catch (HotelException e)
            {
                Console.WriteLine(e.Message);
            }
            catch
            {
                Console.WriteLine("Invalid input...");
            }
        }
    }
}
