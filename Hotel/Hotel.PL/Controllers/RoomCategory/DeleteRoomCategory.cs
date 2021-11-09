using Hotel.BLL.interfaces;

namespace Hotel.PL.Controllers.RoomCategory
{
    public class DeleteRoomCategory : Command
    {
        private IRoomCategoryService _roomCategoryService;
        private IUserService _userService;

        public DeleteRoomCategory(IRoomCategoryService roomCategoryService, IUserService userService) : base(userService)
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