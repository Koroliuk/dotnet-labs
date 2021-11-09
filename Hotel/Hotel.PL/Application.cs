using Hotel.BLL.Utils;
using Hotel.PL.Controllers;

namespace Hotel.PL
{
    internal static class Application
    {
        private static void Main()
        { 
            DependencyProvider.Init();
            Commander.Execute();
        }
    }
}
