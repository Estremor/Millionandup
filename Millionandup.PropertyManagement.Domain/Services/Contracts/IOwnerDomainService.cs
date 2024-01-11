using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Domain.Services.Contracts
{
    public interface IOwnerDomainService : IDomainService
    {
        #region Contract
        Task<ActionResult> SaveAsync(Owner owner);
        #endregion
    }
}
