using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Millionandup.PropertyManagement.Api.Filters;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Millionandup.PropertyManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ODataController
    {
        #region Fields
        private readonly IPropertyAppService _propertyAppService;
        #endregion

        #region C'tor
        public PropertyController(IPropertyAppService propertyAppService) => _propertyAppService = propertyAppService;
        #endregion


        #region Methods
        // GET: api/<PropertyController>
        [HttpGet]
        [EnableQuery]
        public IEnumerable<PropertyReadDto> Get()
        {
            return _propertyAppService.List();
        }

        // POST api/<PropertyController>
        [HttpPost]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Post(PropertyDataDto propertyData)
        {
            await _propertyAppService.SavePropertyAsync(propertyData);
            return Ok();
        }

        // PUT api/<PropertyController>/5
        [HttpPut]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Put(PropertyTraceDto propertyTrace)
        {
            await _propertyAppService.UpdatePropertyAsync(propertyTrace);
            return Ok();
        }

        [HttpPatch]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> UpdatePrice(PriceDto priceDto)
        {
            await _propertyAppService.UpdatePriceAsync(priceDto);
            return Ok();
        }
        #endregion
    }
}
