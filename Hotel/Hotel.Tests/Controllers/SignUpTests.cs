using System;
using System.IO;
using Hotel.BLL.DTO;
using Hotel.BLL.interfaces;
using Hotel.DAL.Entities;
using Hotel.PL.Controllers.Account;
using Moq;
using NUnit.Framework;

namespace Hotel.Tests.Controllers
{
    public class SignUpTests
    {
        [Test]
        public void Execute()
        {
            var input = new StringReader("user\npasswd\nUser");
            Console.SetIn(input);
            var mockUserService = new Mock<IUserService>();
            mockUserService
                .Setup(s => s.SignUp(It.IsAny<UserDto>()))
                .Verifiable();
            mockUserService
                .Setup(s => s.FindByLogin("user"))
                .Returns((User)null);
            var signUp = new SignUp(mockUserService.Object);
            
            signUp.Execute();
            
            mockUserService.Verify(s => s.SignUp(It.IsAny<UserDto>()), Times.Once);
        }
    }
}