using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.RoomCategory
{
    public class DeleteRoomCategory : Command
    {
        private readonly IRoomCategoryService _roomCategoryService;

        public DeleteRoomCategory(IRoomCategoryService roomCategoryService, IUserService userService) : base(
            userService)
        {
            _roomCategoryService = roomCategoryService;
        }

        public override void Execute()
        {
            try
            {
                AuthorizeAsAdmin();
                Console.Write("Enter a category Id to delete: ");
                var categoryId = Convert.ToInt32(Console.ReadLine());
                
                _roomCategoryService.DeleteById(categoryId);
            }
            catch (HotelException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
