using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Domain.Base.Contract;

namespace Millionandup.PropertyManagement.Aplication.AplicationService.Contract
{
    public interface IPropertyImageAppService : IAppService
    {
        #region Contract
        Task AddImgeToPropertyAsync(ImageDto image);
        #endregion
    }
}
