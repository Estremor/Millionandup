using Microsoft.EntityFrameworkCore;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.Errors;
using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.Services.Contracts;
using System.Net;

namespace Millionandup.PropertyManagement.Aplication.AplicationService
{
    public class LoginAppService : AppService, ILoginAppService
    {
        #region Fileds
        private readonly ILoginDomainService _loginDomainService;
        #endregion

        #region C´tor
        public LoginAppService(DbContext context) : base(context)
        {
            _loginDomainService = Context.GetDomainService<ILoginDomainService>();
        }
        #endregion

        #region Methods
        public async Task<UserDto> LoginUserAsync(string userName, string password)
        {
            ActionResult userResult = await _loginDomainService.FindUserAsync(new User { UserName = userName, Password = password });
            if (userResult.IsSuccessful)
            {
                var user = (User)userResult.Result;
                return new UserDto
                {
                    UserName = user.UserName,
                    Token = _loginDomainService.CreateToken(user)
                };
            }
            throw new RestException(HttpStatusCode.NotFound, new { Messages = userResult.ErrorMessage });
        }
        #endregion
    }
}
