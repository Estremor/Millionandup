using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Domain.Services.Contracts
{
    public interface IPropertyImageDomainService : IDomainService
    {
        #region Contract
        Task<ActionResult> SaveImageAsync(PropertyImage image);
        #endregion
    }
}
