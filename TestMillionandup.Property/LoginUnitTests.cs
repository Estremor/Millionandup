﻿using Moq;
using NUnit.Framework;
using Millionandup.PropertyManagement.Api.Controllers;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;

namespace TestMillionandup.Property
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_Login_User_Not_Valid()
        {
            var mock = new Mock<ILoginAppService>();
            string userName = "test";
            string password = "123";
            mock.Setup(sp => sp.LoginUserAsync(userName, password)).Returns(Task.FromResult(new UserDto { Token = "", UserName = "test" }));

            LoginController controller = new(mock.Object);
            var dto = await controller.Get(userName, password);

            Assert.IsTrue(string.IsNullOrWhiteSpace(dto.Token), "usuario se pudo loguear con datos errados");
        }
        [Test]
        public async Task Test_Login_User_Valid()
        {
            var mock = new Mock<ILoginAppService>();
            string userName = "test";
            string password = "123";
            mock.Setup(sp => sp.LoginUserAsync(userName, password)).Returns(Task.FromResult(new UserDto { Token = "ste es mi token", UserName = "test" }));

            LoginController controller = new(mock.Object);
            var dto = await controller.Get(userName, password);

            Assert.That(!string.IsNullOrWhiteSpace(dto.Token), Is.True, "usuario no se pudo loguear verfificar login");
        }
    }
}
