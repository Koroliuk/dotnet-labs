using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
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
            _unitOfWork.RoomCategories.Delete(id);
            _unitOfWork.Save();
        }
    }
}