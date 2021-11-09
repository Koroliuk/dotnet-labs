using System;

namespace Hotel.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public OrderType Type { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
    }

    public enum OrderType
    {
        Booked,
        Paid
    }
}
