using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Room
{
    public class EditRoom : Command
    {
        private readonly IRoomService _roomService;

        public EditRoom(IUserService userService, IRoomService roomService) : base(userService)
        {
            _roomService = roomService;
        }

        public override void Execute()
        {
            try
            {
                AuthorizeAsAdmin();
                
                Console.Write("Enter a room Id to edit: ");
                var roomId = Convert.ToInt32(Console.ReadLine());

                var isRoomExists = _roomService.IsExistById(roomId);
                if (!isRoomExists)
                {
                    throw new HotelException("There is no room room with id = " + roomId);
                }

                Console.Write("Enter a new categoryId for room: ");
                var categoryId = Convert.ToInt32(Console.ReadLine());

                var roomDto = new RoomDto() {CategoryId = categoryId};

                _roomService.Update(roomId, roomDto);
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