using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(RoomCategoryDto roomCategoryDto)
        {
            if (roomCategoryDto.Name == null || roomCategoryDto.Name.Equals(string.Empty) ||
                roomCategoryDto.Description == null || roomCategoryDto.Capacity == 0 ||
                roomCategoryDto.PricePerDay == 0)
            {
                throw new HotelException("Invalid input");
            }
            
            var roomCategory = new RoomCategory
            {
                Name = roomCategoryDto.Name,
                PricePerDay = roomCategoryDto.PricePerDay,
                Capacity = roomCategoryDto.Capacity,
                Description = roomCategoryDto.Description
            };

            _unitOfWork.RoomCategories.Create(roomCategory);
            _unitOfWork.Save();
        }

        public RoomCategory FindById(int id)
        {
            return _unitOfWork.RoomCategories.FindById(id);
        }

        public bool IsExistById(int id)
        {
            return FindById(id) != null;
        }

        public void Update(int categoryId, RoomCategoryDto roomCategoryDto)
        {
            if (roomCategoryDto.Name == null || roomCategoryDto.Name.Equals(string.Empty) ||
                roomCategoryDto.Description == null || roomCategoryDto.Capacity == 0 ||
                roomCategoryDto.PricePerDay == 0)
            {
                throw new HotelException("Invalid input");
            }
            
            var roomCategory = FindById(categoryId);
            roomCategory.Name = roomCategoryDto.Name;
            roomCategory.PricePerDay = roomCategoryDto.PricePerDay;
            roomCategory.Capacity = roomCategoryDto.Capacity;
            roomCategory.Description = roomCategoryDto.Description;

            _unitOfWork.RoomCategories.Update(roomCategory);
            _unitOfWork.Save();
        }

        public void DeleteById(int id)
        {
            var isCategoryExists = IsExistById(id);
            if (!isCategoryExists)
            {
                throw new HotelException("There is no room category with id = " + id);
            }
            _unitOfWork.RoomCategories.Delete(id);
            _unitOfWork.Save();
        }
    }
}
