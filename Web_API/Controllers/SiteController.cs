using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<SiteController> _logger;
        public SiteController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<SiteController> logger) {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
        
        }
        [HttpPut]
        [Route("createsite")]
        public async Task<IActionResult> InsertSite(SiteModel model)=>
            Ok(await _unitOfWork.Sites.InsertAsync(model).ConfigureAwait(true));
        [HttpPut]
        [Route("updatesite")]
        public async Task<IActionResult> UpdateSite(SiteModel model) =>
            Ok(await _unitOfWork.Sites.UpdateAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("searchsite")]
        public async Task<IActionResult> SearchSite(SearchParams model) =>
            Ok(await _unitOfWork.Sites.GetAllAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("getsitedetailsbyid")]
        public async Task<IActionResult> GetSiteDetailsById(Guid sitetId) =>
           Ok(await _unitOfWork.Sites.GetSiteDetailsById(sitetId).ConfigureAwait(true));
        [HttpGet]
        [Route("AllSites")]
        public async Task<IActionResult> GetAllSites()
         => Ok(await _unitOfWork.Sites.GetAllSites().ConfigureAwait(true));


    }
}
