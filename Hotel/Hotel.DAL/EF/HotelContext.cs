using System.Configuration;
using Hotel.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.EF
{
    public sealed class HotelContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get;set; }
        public DbSet<Room> Rooms { get; set;}
        public DbSet<RoomCategory> RoomCategories { get; set;}

        public HotelContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel;Username=hotel;Password=hotel");
        }
    }
}
