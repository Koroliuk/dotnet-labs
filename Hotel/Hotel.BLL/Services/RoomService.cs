using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(RoomDto roomDto)
        {
            var roomCategory = _unitOfWork.RoomCategories.FindById(roomDto.CategoryId);

            if (roomCategory == null)
            {
                throw new HotelException("Room category with id = " + roomDto.CategoryId + " was not found");
            }

            var room = new Room {RoomCategory = roomCategory};

            _unitOfWork.Rooms.Create(room);
            _unitOfWork.Save();
        }

        public Room FindById(int id)
        {
            return _unitOfWork.Rooms.FindById(id);
        }

        public bool IsExistById(int id)
        {
            return FindById(id) != null;
        }

        public void Update(int roomId, RoomDto roomDto)
        {
            var isRoomExists = IsExistById(roomId);
            if (!isRoomExists)
            {
                throw new HotelException("There is no room room with id = " + roomId);
            }
            
            var roomCategory = _unitOfWork.RoomCategories.FindById(roomDto.CategoryId);

            if (roomCategory == null)
            {
                throw new HotelException("Room category with id = " + roomDto.CategoryId + " was not found");
            }

            var room = _unitOfWork.Rooms.FindById(roomId);
            room.RoomCategory = roomCategory;

            _unitOfWork.Rooms.Update(room);
            _unitOfWork.Save();
        }

        public void DeleteById(int id)
        {
            var isRoomExists = IsExistById(id);
            if (!isRoomExists)
            {
                throw new HotelException("There is no room room with id = " + id);
            }
            
            _unitOfWork.Rooms.Delete(id);
            _unitOfWork.Save();
        }
    }
}
