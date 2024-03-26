using BAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BAL.RequestModels;
using BAL.Constant;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using BAL.Request;


namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<ProviderController> _logger;
        public ProviderController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<ProviderController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;

        }
        [HttpPost]
        [Route("createprovider")]
        public async Task<IActionResult> InsertProvider(ProviderModel model) =>
            Ok(await _unitOfWork.Providers.InsertAsync(model).ConfigureAwait(true));



        [HttpPut]
        [Route("updateprovider")]
        public async Task<IActionResult> UpdateProvider(ProviderModel model) =>
            Ok(await _unitOfWork.Providers.UpdateAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("searchprovider")]
        public async Task<IActionResult> SearchProvider(SearchProviderParams model) =>
            Ok(await _unitOfWork.Providers.GetAllAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("getproviderdetailsbyid")]
        public async Task<IActionResult> GetProviderDetailsById(Guid providerId) =>
            Ok(await _unitOfWork.Providers.GetProviderDetailsById(providerId).ConfigureAwait(true));
        [HttpPut]
        [Route("deletepatient")]
        public async Task<IActionResult> DeleteProvider([FromForm, Required] Guid providerId) =>
            Ok(await _unitOfWork.Providers.DeleteAsync(providerId).ConfigureAwait(true));

        [HttpGet]
        [Route("AllProviders")]
        public async Task<IActionResult> GetAllProviders()
          => Ok(await _unitOfWork.Providers.GetAllProviders().ConfigureAwait(true));
    }
}
