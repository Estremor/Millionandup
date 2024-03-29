﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Millionandup.PropertyManagement.Api.Filters;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Aplication.AplicationService.Contract;

namespace Millionandup.PropertyManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        #region Fields
        private readonly IPropertyImageAppService _imageAppService;
        #endregion

        #region C'tor
        public ImageController(IPropertyImageAppService imageAppService) => _imageAppService = imageAppService;
        #endregion

        #region Methods
        // POST api/<ImageController>
        [HttpPost]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Post(ImageDto image)
        {
            await _imageAppService.AddImgeToPropertyAsync(image);
            return Ok();
        }
        #endregion
    }
}
