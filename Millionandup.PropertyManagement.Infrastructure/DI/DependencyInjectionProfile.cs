using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Millionandup.PropertyManagement.Domain.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Millionandup.PropertyManagement.Domain.Services;
using Millionandup.PropertyManagement.Domain.IRepository;
using Millionandup.PropertyManagement.Domain.Services.Contracts;
using Millionandup.PropertyManagement.Infrastructure.Repository;
using Millionandup.PropertyManagement.Aplication.AplicationService;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;


namespace Millionandup.PropertyManagement.Infrastructure.DI
{
    /// <summary>
    /// Contiene la Configuracion de la injeccion de dependencias
    /// </summary>
    public static class DependencyInjectionProfile
    {
        /// <summary>
        /// Registra Las dependencias, como se resuelven
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterProfile(IServiceCollection services, ConfigurationManager configuration)
        {
            #region Context
            DBExtensions.services = services;
            /*Registramos el contexto*/
            services.AddTransient<Microsoft.EntityFrameworkCore.DbContext, DataPersistence.MillionPropertyDbContext>(s =>
            {
                Base.DbSettings settings = s.GetService<Base.DbSettings>();
                return new DataPersistence.MillionPropertyDbContext(settings);
            });
            #endregion

            #region Authentication
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration.GetSection("SecurityKey")?.Value ?? string.Empty));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });
            #endregion

            #region Repositories
            /*Resolvemos los repositorios Genericos*/
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            #endregion

            #region Application

            services.AddTransient<IOwnerAppService, OwnerAppService>();
            services.AddTransient<ILoginAppService, LoginAppService>();
            services.AddTransient<IPropertyAppService, PropertyAppService>();
            services.AddTransient<IPropertyImageAppService, PropertyImageAppService>();
            #endregion

            #region Domain
            services.AddTransient<IOwnerDomainService, OwnerDomainService>();
            services.AddTransient<ILoginDomainService, LoginDomainService>();
            services.AddTransient<IPropertyDomainService, PropertyDomainService>();
            services.AddTransient<IPropertyImageDomainService, PropertyImageDomainService>();
            services.AddTransient<IPropertyTraceDomainService, PropertyTraceDomainService>();
            #endregion
        }
    }
}
