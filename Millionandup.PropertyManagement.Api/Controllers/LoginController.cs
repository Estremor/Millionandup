﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;
using Millionandup.PropertyManagement.Aplication.Dtos;

namespace Millionandup.PropertyManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        #region Fields
        private readonly ILoginAppService _loginAppService;
        #endregion

        #region C'tor
        public LoginController(ILoginAppService loginAppService) => _loginAppService = loginAppService;
        #endregion

        #region Methods
        // GET api/<LoginController>/5
        [HttpGet]
        [Route(nameof(Get))]
        [AllowAnonymous]
        public async Task<UserDto> Get(string userName, string password)
        {
            return await _loginAppService.LoginUserAsync(userName, password);
        }
        #endregion

    }
}
