using Hotel.BLL.DTO;
using Hotel.BLL.Services;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hotel.Tests.Services
{
    public class UserServiceTests
    {
        [Test]
        public void SignUp_WithCorrectInput()
        {
            var userDto = new UserDto
            {
                Login = "user",
                PasswordHash = "sfsdfweg34gerfef",
                Role = "Admin"
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Users.Create(It.IsAny<User>()))
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Save())
                .Verifiable();
            mockUnitOfWork
                .Setup(u => u.Users.FindByLogin(userDto.Login))
                .Returns((User) null);
            var userService = new UserService(mockUnitOfWork.Object);

            userService.SignUp(userDto);
            
            mockUnitOfWork.Verify(u => u.Users.Create(It.IsAny<User>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }
        
        [Test]
        public void SignUp_WithInvalidInput_ThrowsHostelException()
        {
            var userDto = new UserDto
            {
                Login = "user",
                PasswordHash = null,
                Role = "Admin"
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => userService.SignUp(userDto));
        }

        [Test]
        public void SignUp_WithInvalidRole_ThrowsHostelException()
        {
            var userDto = new UserDto
            {
                Login = "user",
                PasswordHash = "asdasdasf",
                Role = "asasd"
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => userService.SignUp(userDto));
        }
        
        [Test]
        public void SignUp_UserWithSuchLoginAlreadyExists_ThrowsHostelException()
        {
            var userDto = new UserDto
            {
                Login = "user",
                PasswordHash = "asdasdasf",
                Role = "asasd"
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Users.FindByLogin(userDto.Login))
                .Returns(new User());
            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<HotelException>(() => userService.SignUp(userDto));
        }
        
        [Test]
        public void FindByLogin()
        {
            const string login = "user1";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Users.FindByLogin(login)).Verifiable();
            var userService = new UserService(mockUnitOfWork.Object);

            userService.FindByLogin(login);

            mockUnitOfWork.Verify(u => u.Users.FindByLogin(login), Times.Once);
        }

        [Test]
        public void IsExistByLogin_ReturnedTrue()
        {
            const string login = "user1";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Users.FindByLogin(login)).Returns(new User());
            var userService = new UserService(mockUnitOfWork.Object);

            Assert.IsTrue(userService.IsExistByLogin(login));
        }

        [Test]
        public void IsExistByLogin_ReturnedFalse()
        {
            const string login = "user1";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Users.FindByLogin(login)).Returns((User)null);
            var userService = new UserService(mockUnitOfWork.Object);

            Assert.IsFalse(userService.IsExistByLogin(login));
        }
    }
}
