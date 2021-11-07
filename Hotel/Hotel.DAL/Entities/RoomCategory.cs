namespace Hotel.DAL.Entities
{
    public class RoomCategory
    {
        public int Id { get; set; }
        public decimal PricePerDay { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }
}