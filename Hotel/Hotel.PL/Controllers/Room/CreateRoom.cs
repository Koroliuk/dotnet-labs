using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Room
{
    public class CreateRoom : Command
    {
        private readonly IRoomService _roomService;

        public CreateRoom(IUserService userService, IRoomService roomService) : base(userService)
        {
            _roomService = roomService;
        }

        public override void Execute()
        {
            try
            {
                AuthorizeAsAdmin();

                Console.Write("Enter a categoryId for room: ");
                var categoryId = Convert.ToInt32(Console.ReadLine());

                var roomDto = new RoomDto {CategoryId = categoryId};

                _roomService.Create(roomDto);
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
