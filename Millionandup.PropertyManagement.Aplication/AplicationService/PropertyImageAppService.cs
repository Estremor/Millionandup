using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.Errors;
using Millionandup.PropertyManagement.Domain.IRepository;
using Millionandup.PropertyManagement.Domain.Services.Contracts;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;

namespace Millionandup.PropertyManagement.Aplication.AplicationService
{
    public class PropertyImageAppService : AppService, IPropertyImageAppService
    {
        #region Fields
        private readonly IPropertyImageDomainService _propertyImageDomainServ;
        private readonly IRepository<Property> _propertyRepo;
        private readonly IMapper _mapper;
        #endregion

        #region C´tor
        public PropertyImageAppService(DbContext context, IMapper mapper) : base(context)
        {
            _propertyImageDomainServ = Context.GetDomainService<IPropertyImageDomainService>();
            _propertyRepo = Context.GetRepository<IRepository<Property>>();
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task AddImgeToPropertyAsync(ImageDto image)
        {
            PropertyImage entity = _mapper.Map<PropertyImage>(image);
            entity.IdProperty = _propertyRepo.List(x => x.CodeInternal == image.InernalCode)?.FirstOrDefault()?.IdProperty ?? Guid.Empty;
            ActionResult result = await _propertyImageDomainServ.SaveImageAsync(entity);

            if (!result.IsSuccessful)
                throw new RestException(HttpStatusCode.InternalServerError, new { Messages = result.ErrorMessage });
            await Context.SaveChangesAsync();
        }
        #endregion
    }
}
