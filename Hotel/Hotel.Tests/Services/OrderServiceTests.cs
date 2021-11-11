using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.BLL.Services;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hotel.Tests.Services
{
    public class OrderServiceTests
    {
        private List<Room> _rooms;
        private List<Order> _orders;

        [SetUp]
        public void SetUp()
        {
            var roomCategory1 = new RoomCategory
            {
                Id = 1,
                Capacity = 2,
                Name = "name1",
                Description = "Some text",
                PricePerDay = 1234m
            };
            var roomCategory2 = new RoomCategory
            {
                Id = 2,
                Capacity = 3,
                Name = "name2",
                Description = "Some text and text",
                PricePerDay = 11000.50m
            };

            _rooms = new List<Room>
            {
                new() {Id = 1, RoomCategory = roomCategory1},
                new() {Id = 2, RoomCategory = roomCategory1},
                new() {Id = 3, RoomCategory = roomCategory2}
            };

            _orders = new List<Order>
            {
                new()
                {
                    Id = 1, Room = _rooms[0],
                    Start = DateTime.MinValue,
                    End = DateTime.MaxValue,
                    User = new User(),
                    Type = OrderType.Paid
                },
                new()
                {
                    Id = 2,
                    Room = _rooms[1],
                    Start = DateTime.MinValue,
                    End = DateTime.MaxValue,
                    User = new User(),
                    Type = OrderType.Booked
                }
            };
        }

        [Test]
        public void BookRoomById_WithCorrectInput()
        {
            const int roomId = 3;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(_rooms[2]);
            mockUnitOfWork
                .Setup(u => u.Orders.Find(It.IsAny<Func<Order, bool>>()))
                .Returns(_orders);
            mockUnitOfWork
                .Setup(u => u.Rooms.GetAll())
                .Returns(_rooms);
            mockUnitOfWork
                .Setup(u => u.Orders.Create(It.IsAny<Order>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var orderService = new OrderService(mockUnitOfWork.Object);

            orderService.BookRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now.AddDays(24));

            mockUnitOfWork
                .Verify(u => u.Orders.Create(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork
                .Verify(u => u.Save(), Times.Once);
        }

        [Test]
        public void BookRoomById_WithIncorrectDateRange()
        {
            const int roomId = 3;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() =>
                orderService.BookRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now));
        }

        [Test]
        public void BookRoomById_WithIncorrectRoom()
        {
            const int roomId = 3;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns((Room) null);
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() =>
                orderService.BookRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now.AddDays(24)));
        }

        [Test]
        public void BookRoomById_WithAlreadyOccupiedRoom()
        {
            const int roomId = 2;
            var roomList = new List<Room> {_rooms[0], _rooms[1]};
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(_rooms[2]);
            mockUnitOfWork
                .Setup(u => u.Orders.Find(It.IsAny<Func<Order, bool>>()))
                .Returns(_orders);
            mockUnitOfWork
                .Setup(u => u.Rooms.GetAll())
                .Returns(roomList);
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() =>
                orderService.BookRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now.AddDays(24)));
        }
        
        [Test]
        public void RentRoomById_WithCorrectInput()
        {
            const int roomId = 3;
            const decimal expectedPrice = 132006m;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(_rooms[2]);
            mockUnitOfWork
                .Setup(u => u.Orders.Find(It.IsAny<Func<Order, bool>>()))
                .Returns(_orders);
            mockUnitOfWork
                .Setup(u => u.Rooms.GetAll())
                .Returns(_rooms);
            mockUnitOfWork
                .Setup(u => u.Orders.Create(It.IsAny<Order>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var orderService = new OrderService(mockUnitOfWork.Object);

            var actualPrice = orderService.RentRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now.AddDays(24));

            mockUnitOfWork
                .Verify(u => u.Orders.Create(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork
                .Verify(u => u.Save(), Times.Once);
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [Test]
        public void RentRoomById_WithIncorrectDateRange()
        {
            const int roomId = 3;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() =>
                orderService.RentRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now));
        }

        [Test]
        public void RentRoomById_WithIncorrectRoom()
        {
            const int roomId = 3;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns((Room) null);
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() =>
                orderService.RentRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now.AddDays(24)));
        }

        [Test]
        public void RentRoomById_WithAlreadyOccupiedRoom()
        {
            const int roomId = 2;
            var roomList = new List<Room> {_rooms[0], _rooms[1]};
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Rooms.FindById(roomId))
                .Returns(_rooms[2]);
            mockUnitOfWork
                .Setup(u => u.Orders.Find(It.IsAny<Func<Order, bool>>()))
                .Returns(_orders);
            mockUnitOfWork
                .Setup(u => u.Rooms.GetAll())
                .Returns(roomList);
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() =>
                orderService.RentRoomById(roomId, new User(), DateTime.Now.AddDays(12), DateTime.Now.AddDays(24)));
        }

        [Test]
        public void GetFreeRooms_WithCorrectInput()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Orders.Find(It.IsAny<Func<Order, bool>>()))
                .Returns(_orders);
            mockUnitOfWork
                .Setup(u => u.Rooms.GetAll())
                .Returns(_rooms);

            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.AreEqual(1,
                orderService.GetFreeRooms(DateTime.Now.AddYears(1), DateTime.MaxValue).Count());
        }

        [Test]
        public void GetFreeRooms_WithIncorrectInput()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => orderService.GetFreeRooms(DateTime.Now.AddDays(-2), DateTime.MaxValue));
        }

        [Test]
        public void FindById()
        {
            const int orderId = 142;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Orders.FindById(orderId))
                .Verifiable();
            var orderService = new OrderService(mockUnitOfWork.Object);

            orderService.FindById(orderId);
            mockUnitOfWork
                .Verify(u => u.Orders.FindById(orderId), Times.Once);
        }

        [Test]
        public void IsExistsById_ReturnTrue()
        {
            const int orderId = 142;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Orders.FindById(orderId))
                .Returns(new Order());
            var orderService = new OrderService(mockUnitOfWork.Object);

            var result = orderService.IsExistsById(orderId);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsExistsById_ReturnFalse()
        {
            const int orderId = 142;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Orders.FindById(orderId))
                .Returns((Order) null);
            var orderService = new OrderService(mockUnitOfWork.Object);

            var result = orderService.IsExistsById(orderId);

            Assert.IsFalse(result);
        }

        [Test]
        public void TransformFromBookedToRentedById_WithCorrect()
        {
            const int orderId = 142;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Orders.FindById(orderId))
                .Returns(new Order
                {
                    Id = 1,
                    Room = new Room()
                    {
                        RoomCategory = new RoomCategory()
                        {
                            PricePerDay = 1200m
                        }
                    },
                    Start = DateTime.Now.AddDays(1),
                    End = DateTime.Now.AddDays(4),
                    User = new User(),
                    Type = OrderType.Paid
                });
            mockUnitOfWork
                .Setup(u => u.Orders.Update(It.IsAny<Order>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            var orderService = new OrderService(mockUnitOfWork.Object);
            const decimal expectedResult = 3600m;

            var actualResult = orderService.TransformFromBookedToRentedById(orderId);

            mockUnitOfWork.Verify(u => u.Orders.Update(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TransformFromBookedToRentedById_WithIncorrect()
        {
            const int orderId = 142;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Orders.FindById(orderId))
                .Returns((Order) null);

            var orderService = new OrderService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => orderService.TransformFromBookedToRentedById(orderId));
        }
    }
}
