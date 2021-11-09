using Hotel.BLL.interfaces;

namespace Hotel.PL.Controllers.RoomCategory
{
    public class EditRoomCategory : Command
    {
        private IRoomCategoryService _roomCategoryService;
        private IUserService _userService;

        public EditRoomCategory(IRoomCategoryService roomCategoryService, IUserService userService) : base(userService)
        {
            _roomCategoryService = roomCategoryService;
            _userService = userService;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}