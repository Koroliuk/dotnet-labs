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
    }
}