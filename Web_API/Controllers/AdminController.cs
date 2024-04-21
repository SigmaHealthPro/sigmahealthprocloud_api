using BAL.Interfaces;
using BAL.Request;
using BAL.Services;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost]
        [Route("update-facility-status")]
        public async Task<IActionResult> UpdateFacilityStatus([FromForm, Required] Guid facilityId)

       => Ok(await _adminService.UpdateFacilityStatus(facilityId).ConfigureAwait(true));
        [HttpPost]
        [Route("update-organization-status")]
        public async Task<IActionResult> UpdateOrganizationStatus([FromForm, Required] Guid organizationId)

       => Ok(await _adminService.UpdateOrganizationStatus(organizationId).ConfigureAwait(true));
        [HttpPost]
        [Route("update-lovmaster-status")]
        public async Task<IActionResult> UpdateLovMasterStatus([FromForm, Required] Guid  lovMasterId)

         => Ok(await _adminService.UpdateLovMasterStatus(lovMasterId).ConfigureAwait(true));

        [HttpPost]
        [Route("get-lov-master")]
        public async Task<IActionResult> GetLovMasterData([FromBody] GetLovMasterRequest request)

        => Ok(await _adminService.GetLovMasterData(request).ConfigureAwait(true));

        [HttpPost]
        [Route("get-distinct-lovtypes")]
        public async Task<IActionResult> GetDistinctLovTypes()

        => Ok(await _adminService.GetDistinctLovTypes().ConfigureAwait(true));
    }
}
