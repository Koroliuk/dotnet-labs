using Hotel.BLL.DTO;
using Hotel.BLL.Services;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hotel.Tests.Services
{
    public class RoomServiceTests
    {
        [Test]
        public void Create_WithCorrectInput()
        {
            const int roomCategoryId = 2;
            var roomCategory = new RoomCategory
            {
                Id = roomCategoryId,
                Name = "name",
                Description = "Description",
                Capacity = 2,
                PricePerDay = 1400.50m
            };
            var roomDto = new RoomDto
            {
                CategoryId = roomCategoryId
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.RoomCategories.FindById(roomCategoryId))
                .Returns(roomCategory);
            mockUnitOfWork
                .Setup(u => u.Rooms.Create(It.IsAny<Room>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var roomService = new RoomService(mockUnitOfWork.Object);

            roomService.Create(roomDto);

            mockUnitOfWork.Verify(u => u.Rooms.Create(It.IsAny<Room>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Test]
        public void Create_WithInCorrectInput_ThrowsHostelException()
        {
            const int roomCategoryId = 2;
            var roomDto = new RoomDto
            {
                CategoryId = roomCategoryId
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.RoomCategories.FindById(roomCategoryId))
                .Returns((RoomCategory) null);
            var roomService = new RoomService(mockUnitOfWork.Object);
            
            Assert.Throws<HotelException>(() => roomService.Create(roomDto));
        }
        
        [Test]
        public void FindById()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(It.IsAny<int>()))
                .Verifiable();
            var roomService = new RoomService(mockUnitOfWork.Object);

            roomService.FindById(1234);

            mockUnitOfWork.Verify(u => u.Rooms.FindById(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void IsExistById_ReturnTrue()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(It.IsAny<int>()))
                .Returns(new Room());
            var roomService = new RoomService(mockUnitOfWork.Object);

            var result = roomService.IsExistById(1234);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsExistById_ReturnFalse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(It.IsAny<int>()))
                .Returns((Room) null);
            var roomService = new RoomService(mockUnitOfWork.Object);

            var result = roomService.IsExistById(1234);

            Assert.IsFalse(result);
        }

        [Test]
        public void Update_WithCorrectInput()
        {
            const int roomId = 1;
            const int roomCategoryId = 2;
            var roomCategory = new RoomCategory
            {
                Id = roomCategoryId,
                Name = "name",
                Description = "Description",
                Capacity = 2,
                PricePerDay = 1400.50m
            };
            var roomDto = new RoomDto
            {
                CategoryId = roomCategoryId
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(new Room {Id = roomId});
            mockUnitOfWork
                .Setup(u => u.RoomCategories.FindById(roomCategoryId))
                .Returns(roomCategory);
            mockUnitOfWork
                .Setup(u => u.Rooms.Update(It.IsAny<Room>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var roomService = new RoomService(mockUnitOfWork.Object);

            roomService.Update(roomId, roomDto);

            mockUnitOfWork.Verify(u => u.Rooms.Update(It.IsAny<Room>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Test]
        public void Update_WithIncorrectRoomId()
        {
            const int roomId = 1;
            const int roomCategoryId = 2;
            var roomDto = new RoomDto
            {
                CategoryId = roomCategoryId
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns((Room) null);
            var roomService = new RoomService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomService.Update(roomId, roomDto));
        }
        
        [Test]
        public void Update_WithIncorrectRoomDto()
        {
            const int roomId = 1;
            const int roomCategoryId = 6;
            var roomDto = new RoomDto
            {
                CategoryId = roomCategoryId
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(new Room {Id = roomId});
            mockUnitOfWork
                .Setup(u => u.RoomCategories.FindById(roomCategoryId))
                .Returns((RoomCategory) null);
            var roomService = new RoomService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomService.Update(roomId, roomDto));
        }

        [Test]
        public void DeleteById_WithCorrectInput()
        {
            const int roomId = 1234;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(new Room() {Id = roomId});
            mockUnitOfWork
                .Setup(u => u.Rooms.Delete(roomId))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var roomService = new RoomService(mockUnitOfWork.Object);

            roomService.DeleteById(roomId);

            mockUnitOfWork.Verify(u => u.Rooms.Delete(roomId), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Test]
        public void DeleteById_WithIncorrectUInput_ThrowsHostelException()
        {
            const int roomId = 1234;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns((Room) null);
            var roomService = new RoomService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => roomService.DeleteById(roomId));
        }
    }
}
