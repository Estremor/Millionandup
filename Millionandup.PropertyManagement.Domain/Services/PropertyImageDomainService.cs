﻿using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.IRepository;
using Millionandup.PropertyManagement.Domain.Services.Contracts;

namespace Millionandup.PropertyManagement.Domain.Services
{
    public class PropertyImageDomainService : DomainService, IPropertyImageDomainService
    {
        #region Fields
        private readonly IRepository<PropertyImage> _imageRepo;
        private readonly IRepository<Property> _propertyRepo;
        #endregion

        #region C´tor
        public PropertyImageDomainService(IRepository<PropertyImage> imageRepo, IRepository<Property> propertyRepo)
        {
            _imageRepo = imageRepo;
            _propertyRepo = propertyRepo;
        }
        #endregion

        #region Methods
        public async Task<ActionResult> SaveImageAsync(PropertyImage image)
        {
            var propertyResult = _propertyRepo.Entity.Find(image.IdProperty);
            if (propertyResult == null) return new ActionResult { IsSuccessful = false, ErrorMessage = "No existe una propiedad con el identificador enviado" };
            await _imageRepo.InsertAsync(image);
            return new ActionResult { IsSuccessful = true };
        }
        #endregion
    }
}
