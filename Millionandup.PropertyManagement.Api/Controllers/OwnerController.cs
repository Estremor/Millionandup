using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Millionandup.PropertyManagement.Api.Filters;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;
using Millionandup.PropertyManagement.Aplication.Dtos;

namespace Millionandup.PropertyManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        #region Fields
        private readonly IOwnerAppService _ownerAppService;
        #endregion

        #region C´tor
        public OwnerController(IOwnerAppService ownerAppService)
        {
            _ownerAppService = ownerAppService;
        }
        #endregion

        #region Methods

        // POST api/<OwnerController>
        [HttpPost]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Post(OwnerDto owner)
        {
            await _ownerAppService.SaveAsync(owner);
            return Ok();
        }
        #endregion
    }
}
