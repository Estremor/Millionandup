using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Aplication.AplicationService.Contract
{
    public interface IOwnerAppService : IAppService
    {
        #region Contract
        Task SaveAsync(OwnerDto owner);
        #endregion
    }
}
