using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.RoomCategory
{
    public class EditRoomCategory : Command
    {
        private readonly IRoomCategoryService _roomCategoryService;

        public EditRoomCategory(IRoomCategoryService roomCategoryService, IUserService userService) : base(userService)
        {
            _roomCategoryService = roomCategoryService;
        }

        public override void Execute()
        {
            try
            {
                AuthorizeAsAdmin();
                Console.Write("Enter a category Id to edit: ");
                var categoryId = Convert.ToInt32(Console.ReadLine());

                var isCategoryExists = _roomCategoryService.IsExistById(categoryId);
                if (!isCategoryExists)
                {
                    throw new HotelException("There is no room category with id = " + categoryId);
                }

                Console.Write("Enter a new category name: ");
                var name = Console.ReadLine();
                Console.Write("Enter a new price per day: ");
                var pricePerDayString = Console.ReadLine();
                Console.Write("Enter a new capacity: ");
                var capacityString = Console.ReadLine();
                Console.Write("Enter a new description: ");
                var description = Console.ReadLine();

                if (name != null && !name.Equals(string.Empty) &&
                    pricePerDayString != null && !pricePerDayString.Equals(string.Empty) &&
                    capacityString != null && !capacityString.Equals(string.Empty) && description != null)
                {
                    var pricePerDay = Convert.ToDecimal(pricePerDayString);
                    var capacity = Convert.ToInt32(capacityString);

                    var roomCategoryDto = new RoomCategoryDto()
                    {
                        Name = name,
                        PricePerDay = pricePerDay,
                        Capacity = capacity,
                        Description = description
                    };

                    _roomCategoryService.Update(categoryId, roomCategoryDto);
                }
                else
                {
                    throw new ArgumentException();
                }
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
