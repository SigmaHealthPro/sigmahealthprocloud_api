﻿using BAL.Interfaces;
using BAL.Request;
using BAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FacilityController : ControllerBase
    {
        private IFacilityService _facilityService;
        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }


        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> FacilitySearch([FromBody] FacilitySearchRequest obj)
         =>Ok(await _facilityService.FacilitySearch(obj).ConfigureAwait(true));


        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> DeleteFacility([FromForm, Required] Guid facilityId)
         =>Ok(await _facilityService.DeleteFacility(facilityId).ConfigureAwait(true));

            
        


    }
}
