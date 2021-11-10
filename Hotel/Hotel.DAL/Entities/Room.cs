namespace Hotel.DAL.Entities
{
    public class Room
    {
        public int Id { get; set; }
        
        #nullable enable
        public RoomCategory? RoomCategory { get; set; }
    }
}
