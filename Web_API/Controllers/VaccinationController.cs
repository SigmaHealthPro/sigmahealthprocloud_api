using BAL.Constant;
using BAL.Repository;
using BAL.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<VaccinationController> _logger;
        public VaccinationController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<VaccinationController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;

        }
        #region Orders
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        [Route("searchorders")]
        public async Task<IActionResult> SearchOrders(SearchOrderParams model)
           => Ok(await _unitOfWork.Orders.GetAllAsync(model).ConfigureAwait(true));
        [HttpPost]
        [Route("getpendingorders")]
        public async Task<IActionResult> GetAllOrders(int pagenumber,int pagesize)
           => Ok(await _unitOfWork.Orders.GetAllOrders(pagenumber,pagesize).ConfigureAwait(true));

        [HttpPut]
        [Route("deleteorder")]
        public async Task<IActionResult> DeleteOrder([FromForm, Required] Guid ordid)
         => Ok(await _unitOfWork.Orders.DeleteAsync(ordid).ConfigureAwait(true));

        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] RespOrderModel obj)
         => Ok(await _unitOfWork.Orders.InsertOrdersAsync(obj).ConfigureAwait(true));

        [HttpPost]
        [Route("editorder")]
        public async Task<IActionResult> EditOrder([FromBody] OrderModel obj)
         => Ok(await _unitOfWork.Orders.UpdateAsync(obj).ConfigureAwait(true));

        [HttpGet]
        [Route("getallfacilities")]
        public async Task<IActionResult> GetAllFacilities()
           => Ok(await _unitOfWork.Facilities.GetAllAsync().ConfigureAwait(true));
        //GetAllFacilitiesbyjurdid
        [HttpPost]
        [Route("getallfacilitiesbyjurdid")]
        public async Task<IActionResult> GetAllFacilitiesbyjurdid(Guid jurdid)
        => Ok(await _unitOfWork.Facilities.GetAllFacilitiesbyjurdid(jurdid).ConfigureAwait(true));
        [HttpGet]
        [Route("getallmvx")]
        public async Task<IActionResult> GetAllMVX()
          => Ok(await _unitOfWork.Orders.GetAllManufacturers().ConfigureAwait(true));
        [HttpGet]
        [Route("getallproducts")]
        public async Task<IActionResult> GetAllProducts()
          => Ok(await _unitOfWork.Products.GetAllAsync().ConfigureAwait(true));

        [HttpPost]
        [Route("getallvaccinesbyfacilityid")]
        public async Task<IActionResult> GetAllVaccinesbyfacilityid(Guid facilityid,int pagenumber, int pagesize,Guid manufacturerid)
         =>     Ok(await _unitOfWork.Products.GetAllVaccinesbyfacilityid(facilityid,pagenumber,pagesize,manufacturerid).ConfigureAwait(true));
        [HttpPost]
        [Route("getaddressbyuserid")]
        public async Task<IActionResult> GetAddressbyUserid(Guid userid)
        => Ok(await _unitOfWork.Orders.GetAddressbyUserid(userid).ConfigureAwait(true));
        [HttpPost]
        [Route("getaddressbyorderid")]
        public async Task<IActionResult> GetAddressbyOrderid(Guid orderid)
        => Ok(await _unitOfWork.Orders.GetAddressbyOrderid(orderid).ConfigureAwait(true));
        [HttpPost]
        [Route("getitemsbyorderid")]
        public async Task<IActionResult> GetOrderItemsbyOrderid(Guid orderid)
      => Ok(await _unitOfWork.Orders.GetOrderdetailsbyOrderid(orderid).ConfigureAwait(true));

        [HttpPost]
        [Route("addvaccines")]
        public async Task<IActionResult> AddVaccines([FromBody] VaccineModel obj)
         => Ok(await _unitOfWork.Products.InsertVaccinedata(obj).ConfigureAwait(true));
        #endregion
    }
}
