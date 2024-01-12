using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Millionandup.PropertyManagement.Api.Controllers;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;

namespace TestMillionandup.Property
{
    [TestFixture]
    public class OwnerControllerUnitTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Test_Owner_Post_Valid()
        {
            var mock = new Mock<IOwnerAppService>();
            OwnerDto owner = new() { Address = "test adress", Birthday = DateTime.Now.AddYears(-20).ToString(), Document = "1010211", Name = "text", Photo = "" };
            mock.Setup(sp => sp.SaveAsync(owner)).Returns(Task.CompletedTask);

            OwnerController controller = new(mock.Object);
            var result = await controller.Post(owner);

            Assert.IsTrue(((StatusCodeResult)result).StatusCode == 200);
        }
    }
}
