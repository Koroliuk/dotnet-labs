using System;
using System.Collections.Generic;
using Hotel.BLL.interfaces;
using Hotel.BLL.Utils;
using Hotel.PL.Controllers.RoomCategory;
using Hotel.PL.Controllers.User;

namespace Hotel.PL.Controllers
{
    public static class Commander
    {
        private static Dictionary<string, Command> Commands { get; set; }

        private static readonly IUserService UserService = DependencyProvider.GetDependency<IUserService>();
        private static readonly IRoomCategoryService RoomCategoryService =
            DependencyProvider.GetDependency<IRoomCategoryService>();

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
                {"delete room category", new DeleteRoomCategory(RoomCategoryService, UserService)}
            };
        }
    }
}