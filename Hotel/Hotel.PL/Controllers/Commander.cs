using System;
using System.Collections.Generic;
using Hotel.BLL.interfaces;
using Hotel.BLL.Utils;
using Hotel.PL.Controllers.Account;
using Hotel.PL.Controllers.Order;
using Hotel.PL.Controllers.Room;
using Hotel.PL.Controllers.RoomCategory;

namespace Hotel.PL.Controllers
{
    public static class Commander
    {
        private static Dictionary<string, Command> Commands { get; set; }

        private static readonly IUserService UserService = DependencyProvider.GetDependency<IUserService>();

        private static readonly IRoomCategoryService RoomCategoryService =
            DependencyProvider.GetDependency<IRoomCategoryService>();

        private static readonly IRoomService RoomService = DependencyProvider.GetDependency<IRoomService>();

        private static readonly IOrderService OrderService = DependencyProvider.GetDependency<IOrderService>();

        public static void Execute()
        {
            Init();
            while (true)
            {
                Console.Write("Enter a command: ");
                var command = Console.ReadLine();
                if (command == null)
                {
                    Console.WriteLine("Please, enter a command");
                    continue;
                }

                if (command == "exit")
                {
                    break;
                }

                if (!Commands.ContainsKey(command))
                {
                    Console.WriteLine("Wrong command!");
                }
                else
                {
                    Commands[command].Execute();
                }
            }
        }

        private static void Init()
        {
            Commands = new Dictionary<string, Command>
            {
                {"signup", new SignUp(UserService)},
                {"create room category", new CreateRoomCategory(RoomCategoryService, UserService)},
                {"edit room category", new EditRoomCategory(RoomCategoryService, UserService)},
                {"delete room category", new DeleteRoomCategory(RoomCategoryService, UserService)},
                {"create room", new CreateRoom(UserService, RoomService)},
                {"edit room", new EditRoom(UserService, RoomService)},
                {"delete room", new DeleteRoom(UserService, RoomService)},
                {"book room", new BookRoom(UserService, OrderService)},
                {"rent room", new RentRoom(UserService, OrderService)},
                {"transform state room", new TransformStateOfRoom(UserService, OrderService)},
                {"show rooms", new ShowRooms(UserService, OrderService)}
            };
        }
    }
}
