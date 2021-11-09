using System.Collections.Generic;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var db = new HotelContext();
            // {
                var user = new User {Login = "sdf", PasswordHash = "sdfsdf", Orders = new List<Order>()};
                
                db.Users.Add(user);
                db.SaveChanges();
            
        }
    }
}