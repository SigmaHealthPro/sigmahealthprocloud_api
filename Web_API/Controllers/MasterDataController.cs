﻿using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<MasterDataController> _logger;
        public MasterDataController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<MasterDataController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
        }
        #region Countries
        [HttpGet]
        [Route("Countries")]
        public async Task<IActionResult> GetAllCountries()
           => Ok(await _unitOfWork.Countries.GetAllCountries().ConfigureAwait(true));

        #endregion

        #region States
        [HttpGet]
        [Route("States")]
        public async Task<IActionResult> GetAllStates()
           => Ok(await _unitOfWork.States.GetAllStates().ConfigureAwait(true));
        [HttpGet]
        [Route("getstatesbycountryid")]
        public async Task<IActionResult> GetStatesByCountry([FromQuery, Required] Guid countryid)
            => Ok(await _unitOfWork.States.GetStatebyCountryid(countryid).ConfigureAwait(true));        
        #endregion

        #region Counties
        
        [HttpGet]
        [Route("getcountiesbystateid")]
        public async Task<IActionResult> GetCountiessByState([FromQuery, Required] Guid stateid)
           => Ok(await _unitOfWork.Counties.GetCountybyStateid(stateid).ConfigureAwait(true));

        #endregion

        #region Cities

        [HttpGet]
        [Route("getcitiesbystateid")]
        public async Task<IActionResult> GetCitiesByState([FromQuery, Required] Guid stateid)
           => Ok(await _unitOfWork.Cities.GetCitybyStateid(stateid).ConfigureAwait(true));

        [HttpGet]
        [Route("getcitiesbystateidandcountyid")]
        public async Task<IActionResult> GetCitiesByStateandCounty([FromQuery, Required] Guid stateid, [FromQuery, Required] Guid countyid)
           => Ok(await _unitOfWork.Cities.GetCitybyStateidandCountyid(stateid,countyid).ConfigureAwait(true));
        #endregion

        #region LOVMaster
        [HttpGet]
        [Route("getlovmasterbylovtype")]
        public async Task<IActionResult> GetLOVMasterbyLOVTypeid([FromQuery, Required] string lovtype)
            => Ok(await _unitOfWork.lOVTypeMaster.GetLOVMasterbyLOVTypeid(lovtype).ConfigureAwait(true));
        #endregion

        #region Contacts
        [HttpGet]
        [Route("getcontactsbycontactid")]
        public async Task<IActionResult> GetContactsbyContactid([FromQuery, Required] string contactid)
            => Ok(await _unitOfWork.Contacts.GetContactsbyContactid(contactid).ConfigureAwait(true));
        #endregion
    }
}
