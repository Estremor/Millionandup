using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.Errors;
using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.Services.Contracts;

namespace Millionandup.PropertyManagement.Aplication.AplicationService
{
    public class OwnerAppService : AppService, IOwnerAppService
    {
        #region Fields
        private readonly IOwnerDomainService _ownerDomainService;
        private readonly IMapper _mapper;
        #endregion

        #region C'tor
        public OwnerAppService(DbContext context, IMapper mapper) : base(context)
        {
            _ownerDomainService = Context.GetDomainService<IOwnerDomainService>();
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task SaveAsync(OwnerDto ownerDto)
        {
            Owner owner = _mapper.Map<Owner>(ownerDto);
            var result = await _ownerDomainService.SaveAsync(owner);
            if (!result.IsSuccessful)
                throw new RestException(System.Net.HttpStatusCode.AlreadyReported, new { Messages = result.ErrorMessage });

            await Context.SaveChangesAsync();
        }
        #endregion
    }
}
