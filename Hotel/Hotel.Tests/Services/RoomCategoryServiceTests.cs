using Hotel.BLL.DTO;
using Hotel.BLL.Services;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hotel.Tests.Services
{
    public class RoomCategoryServiceTests
    {
        [Test]
        public void Create_WithCorrectInput()
        {
            var roomCategoryDto = new RoomCategoryDto
            {
                Name = "name",
                Description = "Description",
                Capacity = 2,
                PricePerDay = 1400.50m
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.RoomCategories.Create(It.IsAny<RoomCategory>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var roomCategoryService = new RoomCategoryService(mockUnitOfWork.Object);

            roomCategoryService.Create(roomCategoryDto);
            
            mockUnitOfWork.Verify(u => u.RoomCategories.Create(It.IsAny<RoomCategory>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }
        
        [Test]
        public void Create_WithInCorrectName_ThrowsHostelException()
        {
            var roomCategoryDto = new RoomCategoryDto
            {
                Name = null,
                Description = "Description",
                Capacity = -1200,
                PricePerDay = 1400.50m
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var roomCategoryService = new RoomCategoryService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomCategoryService.Create(roomCategoryDto));
        }
        
        [Test]
        public void Create_WithInCorrectDescription_ThrowsHostelException()
        {
            var roomCategoryDto = new RoomCategoryDto
            {
                Name = "name",
                Description = "",
                Capacity = -1200,
                PricePerDay = 1400.50m
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var roomCategoryService = new RoomCategoryService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomCategoryService.Create(roomCategoryDto));
        }
        
        [Test]
        public void Create_WithInCorrectCapacity_ThrowsHostelException()
        {
            var roomCategoryDto = new RoomCategoryDto
            {
                Name = "name",
                Description = "Description",
                Capacity = -1200,
                PricePerDay = 1400.50m
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var roomCategoryService = new RoomCategoryService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomCategoryService.Create(roomCategoryDto));
        }
        
           
        [Test]
        public void Create_WithInCorrectPricePerDay_ThrowsHostelException()
        {
            var roomCategoryDto = new RoomCategoryDto
            {
                Name = "name",
                Description = "Description",
                Capacity = 200,
                PricePerDay = 1400.50m
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var roomCategoryService = new RoomCategoryService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomCategoryService.Create(roomCategoryDto));
        }
        
        [Test]
        public void FindById()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.RoomCategories.FindById(It.IsAny<int>()))
                .Verifiable();
            var roomCategoryService = new RoomCategoryService(mockUnitOfWork.Object);

            roomCategoryService.FindById(1234);

            mockUnitOfWork.Verify(u => u.RoomCategories.FindById(It.IsAny<int>()), Times.Once());
        }
    }
}
