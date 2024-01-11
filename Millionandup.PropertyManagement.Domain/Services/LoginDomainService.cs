using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Millionandup.PropertyManagement.Domain.Base;
using Millionandup.PropertyManagement.Domain.Entities;
using Millionandup.PropertyManagement.Domain.IRepository;
using Millionandup.PropertyManagement.Domain.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace Millionandup.PropertyManagement.Domain.Services
{
    public class LoginDomainService : DomainService, ILoginDomainService
    {
        #region Fields
        private readonly IRepository<User> _userRepository;
        private readonly string SecurityKey;
        #endregion

        #region C'tor
        public LoginDomainService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            SecurityKey = configuration.GetSection(nameof(SecurityKey))?.Value ?? string.Empty;
        }
        #endregion

        #region Methods
        public async Task<ActionResult> FindUserAsync(User user)
        {
            var userEntity = await _userRepository.ListAsync(x => x.UserName == user.UserName && x.Password == user.Password);
            return new ActionResult { IsSuccessful = true, Result = userEntity?.FirstOrDefault() ?? new User() };
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };
            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(SecurityKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
