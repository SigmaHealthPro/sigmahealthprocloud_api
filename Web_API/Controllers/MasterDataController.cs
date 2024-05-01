using BAL.Interfaces;
using BAL.Request;
using BAL.Services;
using Data.Models;
using BAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BAL.Constant;
using BAL.Responses;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<MasterDataController> _logger;
        private IMasterDataService _masterdataservice;
        public MasterDataController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<MasterDataController> logger, IMasterDataService masterdataservice)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
            _masterdataservice = masterdataservice;
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
        public async Task<IActionResult> GetContactsbyContactid([FromQuery, Required] Guid contactid)
            => Ok(await _unitOfWork.Contacts.GetContactsbyContactid(contactid).ConfigureAwait(true));
        #endregion

        [HttpPost]
        [Route("generate-next-id")]
        public async Task<IActionResult> GenerateNextId([FromBody] GenerateNextIdRequest obj)
        => Ok(await _masterdataservice.GenerateNextId(obj).ConfigureAwait(true));


        [HttpGet]
        [Route("getlistofpatientduplicatedata")]
        public async Task<IActionResult> GetListOfPatientDuplicateData()
          => Ok(await _masterdataservice.GetListOfPatientDuplicateData().ConfigureAwait(true));
        [HttpGet]
        [Route("getlistofpatientduplicatedatabyid")]
        public async Task<IActionResult> GetListOfPatientDuplicateData(Guid Id)
         => Ok(await _masterdataservice.GetListOfPatientDuplicateDataById(Id).ConfigureAwait(true));

        [HttpGet]
        [Route("getlistofpatientnewdata")]
        public async Task<IActionResult> GetListOfPatientNewData()
          => Ok(await _masterdataservice.GetListOfPatientNewData().ConfigureAwait(true));
        [HttpGet]
        [Route("getlistofpatientnewdatabyid")]
        public async Task<IActionResult> GetListOfPatientNewDataById(Guid Id)
         => Ok(await _masterdataservice.GetListOfPatientNewDataById(Id).ConfigureAwait(true));

        [HttpPost]
        [Route("find-best-match")]
        public async Task<ActionResult<ApiResponse<BestMatchResponse>>> FindBestMatch(BestMatchRequest request)
           => Ok(await _masterdataservice.FindBestMatchPercentage(request).ConfigureAwait(true));
       
    }
}
