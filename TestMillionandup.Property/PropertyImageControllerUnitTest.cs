using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Millionandup.PropertyManagement.Api.Controllers;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;

namespace TestMillionandup.Property
{
    [TestFixture]
    public class PropertyImageControllerUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_Property_Image_Post_Valid()
        {
            var mock = new Mock<IPropertyImageAppService>();
            ImageDto propertyImage = new() { File = "mi imagen en base64", InernalCode = "1234" };
            mock.Setup(c => c.AddImgeToPropertyAsync(propertyImage)).Returns(Task.CompletedTask);

            ImageController controller = new(mock.Object);
            var result = await controller.Post(propertyImage);

            Assert.That(((StatusCodeResult)result).StatusCode == 200, Is.True, "imagen no se pudo insertar");
        }
    }
}
