using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Aplication.AplicationService.Contract
{
    public interface ILoginAppService : IAppService
    {
        #region Contract
        Task<UserDto> LoginUserAsync(string userName, string password);
        #endregion
    }
}
