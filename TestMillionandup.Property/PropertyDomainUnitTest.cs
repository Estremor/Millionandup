using Moq;
using NUnit.Framework;
using Millionandup.PropertyManagement.Domain.Services;
using Millionandup.PropertyManagement.Domain.IRepository;
using entity = Millionandup.PropertyManagement.Domain.Entities;
namespace TestMillionandup.Property
{
    [TestFixture]
    public class PropertyDomainUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_SaveAsync_Property_Valid()
        {
            string propertyStringt = "{  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"CodeInternal\": \"54455545\",  \"Year\": 1995,  \"IdOwner\": \"2E0C9824-C9F4-4F50-3F61-08D9A1B43A87\"}";
            entity.Property property = Newtonsoft.Json.JsonConvert.DeserializeObject<entity.Property>(propertyStringt);
            Mock<IRepository<entity.Property>> mockDomainservice = new();
            Mock<IRepository<entity.Owner>> mockOwner = new();
            mockDomainservice.Setup(sp => sp.InsertAsync(property)).Returns(Task.FromResult(new entity.Property { }));
            mockDomainservice.Setup(sp => sp.List(x => x.CodeInternal == property.CodeInternal)).Returns(new List<entity.Property>());
            mockOwner.Setup(sp => sp.List(x => x.IdOwner == property.IdOwner)).Returns(new List<entity.Owner> { new() { IdOwner = Guid.NewGuid() } });

            PropertyDomainService service = new(mockDomainservice.Object, mockOwner.Object);
            var result = await service.SaveAsync(property);

            Assert.That(result.IsSuccessful, Is.True, "Ocurrio un error al guaradar property");
        }

        [Test]
        public async Task Test_SaveAsync_Property_Not_Valid()
        {
            string propertyStringt = "{  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"CodeInternal\": \"54455545\",  \"Year\": 1995,  \"IdOwner\": \"2E0C9824-C9F4-4F50-3F61-08D9A1B43A87\"}";
            entity.Property property = Newtonsoft.Json.JsonConvert.DeserializeObject<entity.Property>(propertyStringt);
            Mock<IRepository<entity.Property>> mockDomainservice = new();
            Mock<IRepository<entity.Owner>> mockOwner = new();
            mockDomainservice.Setup(sp => sp.InsertAsync(property));
            mockDomainservice.Setup(sp => sp.List(x => x.CodeInternal == property.CodeInternal)).Returns(new List<entity.Property>());
            mockOwner.Setup(sp => sp.List(x => x.IdOwner == property.IdOwner)).Returns(new List<entity.Owner> { });

            PropertyDomainService service = new(mockDomainservice.Object, mockOwner.Object);
            var result = await service.SaveAsync(property);

            Assert.That(!result.IsSuccessful, Is.True, "Ocurrio un error al guaradar property");
        }

        [Test]
        public async Task Test_UpdateAsync_Property_Not_Valid()
        {
            string propertyStringt = "{\"IdProperty\":\"3037AC77-2178-474D-3E6C-08D9A1B640E5\",  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"CodeInternal\": \"54455545\",  \"Year\": 1995,  \"IdOwner\": \"2E0C9824-C9F4-4F50-3F61-08D9A1B43A87\"}";
            entity.Property property = Newtonsoft.Json.JsonConvert.DeserializeObject<entity.Property>(propertyStringt);
            Mock<IRepository<entity.Owner>> mockOwner = new();
            mockOwner.Setup(or => or.Entity.Find(property.IdOwner));
            Mock<IRepository<entity.Property>> mockProperty = new();
            mockProperty.Setup(pr => pr.List(x => x.CodeInternal == property.CodeInternal)).Returns(new List<entity.Property>() { new() { } });
            mockProperty.Setup(pr => pr.UpdateAsync(property));

            PropertyDomainService service = new(mockProperty.Object, mockOwner.Object);
            var result = await service.UpdatePropertyAsync(property);

            Assert.That(!result.IsSuccessful, Is.True, "Ocurrio un error,  property fue Actualizada");
        }


        [Test]
        public async Task Test_UpdatePriceAsync_Property_Valid()
        {
            string propertyStringt = "{  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"CodeInternal\": \"54455545\",  \"Year\": 1995,  \"IdOwner\": \"2E0C9824-C9F4-4F50-3F61-08D9A1B43A87\"}";
            entity.Property property = Newtonsoft.Json.JsonConvert.DeserializeObject<entity.Property>(propertyStringt);
            Mock<IRepository<entity.Property>> mockDomainservice = new();
            Mock<IRepository<entity.Owner>> mockOwner = new();
            mockDomainservice.Setup(sp => sp.UpdateAsync(property)).Returns(Task.FromResult(new entity.Property { }));
            mockDomainservice.Setup(sp => sp.List(x => x.CodeInternal == property.CodeInternal)).Returns(new List<entity.Property> { new() { } });

            PropertyDomainService service = new(mockDomainservice.Object, mockOwner.Object);
            var result = await service.UpdatePriceAsync(property);

            Assert.That(result.IsSuccessful, Is.True, "Ocurrio un error al guaradar property");
        }

        [Test]
        public async Task Test_UpdatePriceAsync_Property_Not_Valid()
        {
            string propertyStringt = "{  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"CodeInternal\": \"54455545\",  \"Year\": 1995,  \"IdOwner\": \"2E0C9824-C9F4-4F50-3F61-08D9A1B43A87\"}";
            entity.Property property = Newtonsoft.Json.JsonConvert.DeserializeObject<entity.Property>(propertyStringt);
            Mock<IRepository<entity.Property>> mockDomainservice = new();
            Mock<IRepository<entity.Owner>> mockOwner = new();
            mockDomainservice.Setup(sp => sp.UpdateAsync(property)).Returns(Task.FromResult(new entity.Property { }));
            mockDomainservice.Setup(sp => sp.List(x => x.CodeInternal == property.CodeInternal)).Returns(new List<entity.Property> { });

            PropertyDomainService service = new(mockDomainservice.Object, mockOwner.Object);
            var result = await service.UpdatePriceAsync(property);

            Assert.That(!result.IsSuccessful, Is.True, "Ocurrio un error se actualizo el precio");
        }
    }
}
