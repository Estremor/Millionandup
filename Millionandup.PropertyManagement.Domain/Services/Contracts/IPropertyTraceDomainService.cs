using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Domain.Services.Contracts
{
    public interface IPropertyTraceDomainService : IDomainService
    {
        #region Contract
        ActionResult RegisterTrace(PropertyTrace trace);
        #endregion
    }
}
