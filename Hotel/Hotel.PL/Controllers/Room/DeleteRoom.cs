using System;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;

namespace Hotel.PL.Controllers.Room
{
    public class DeleteRoom : Command
    {
        private readonly IRoomService _roomService;

        public DeleteRoom(IUserService userService, IRoomService roomService) : base(userService)
        {
            _roomService = roomService;
        }

        public override void Execute()
        {
            try
            {
                AuthorizeAsAdmin();

                Console.Write("Enter a room Id to delete: ");
                var roomId = Convert.ToInt32(Console.ReadLine());
                
                _roomService.DeleteById(roomId);
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
