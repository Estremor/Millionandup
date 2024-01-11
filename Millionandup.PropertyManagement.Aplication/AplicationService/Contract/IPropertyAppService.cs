using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Aplication.AplicationService.Contract
{
    public interface IPropertyAppService : IAppService
    {
        #region Contarct
        Task SavePropertyAsync(PropertyDataDto propertyDto);
        Task UpdatePropertyAsync(PropertyTraceDto traceDto);
        Task UpdatePriceAsync(PriceDto priceDto);
        IQueryable<PropertyReadDto> List();
        #endregion
    }
}
