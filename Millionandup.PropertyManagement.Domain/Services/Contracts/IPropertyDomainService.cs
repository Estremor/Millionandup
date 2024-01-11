using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Domain.Services.Contracts
{
    public interface IPropertyDomainService : IDomainService
    {
        #region Contract
        Task<ActionResult> SaveAsync(Property property);
        Task<ActionResult> UpdatePropertyAsync(Property property);
        Task<ActionResult> UpdatePriceAsync(Property property);
        #endregion
    }
}
