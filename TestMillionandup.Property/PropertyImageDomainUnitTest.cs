using Moq;
using NUnit.Framework;
using Millionandup.PropertyManagement.Domain.IRepository;
using Millionandup.PropertyManagement.Domain.Services;
using _entity = Millionandup.PropertyManagement.Domain.Entities;

namespace TestMillionandup.Property
{
    public class PropertyImageDomainUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_Property_Image_SaveImage_Valid()
        {
            Mock<IRepository<_entity.Property>> mockproperty = new();
            Mock<IRepository<_entity.PropertyImage>> mockImage = new();

            var img = new _entity.PropertyImage { Enabled = true, File = Array.Empty<byte>(), IdProperty = Guid.NewGuid() };
            mockproperty.Setup(c => c.Entity.Find(img.IdProperty)).Returns(new _entity.Property { });

            PropertyImageDomainService service = new(mockImage.Object, mockproperty.Object);
            var result = await service.SaveImageAsync(img);

            Assert.That(result.IsSuccessful, Is.True, "Ocurrio un error al guaradar una imagen");

        }

        [Test]
        public async Task Test_Property_Image_SaveImage_Not_Valid()
        {
            Mock<IRepository<_entity.Property>> mockproperty = new();
            Mock<IRepository<_entity.PropertyImage>> mockImage = new();

            var img = new _entity.PropertyImage { Enabled = true, File = Array.Empty<byte>(), IdProperty = Guid.NewGuid() };
            mockproperty.Setup(c => c.Entity.Find(img.IdProperty));

            PropertyImageDomainService service = new(mockImage.Object, mockproperty.Object);
            var result = await service.SaveImageAsync(img);

            Assert.That(!result.IsSuccessful, Is.True, "Ocurrio un error al guaradar una imagen");

        }
    }
}
