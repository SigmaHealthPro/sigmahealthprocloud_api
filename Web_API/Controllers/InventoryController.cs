using BAL.Constant;
using BAL.Interfaces;
using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private IInventoryService _inventoryService;


        public InventoryController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpPost]
        [Route("createInventory")]
        public async Task<IActionResult> InsertInventory(InventoryModel model) =>
            Ok(await _unitOfWork.Inventorys.InsertAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("searchInventory")]
        public async Task<IActionResult> SearchInventory(SearchInventoryParams model) =>
            Ok(await _unitOfWork.Inventorys.GetAllAsync(model).ConfigureAwait(true));

        [HttpPut]
        [Route("deleteinventory")]
        public async Task<IActionResult> DeleteInventory([FromForm, Required] Guid inventoryId) =>
         Ok(await _unitOfWork.Inventorys.DeleteAsync(inventoryId).ConfigureAwait(true));

        [HttpGet]
        [Route("inventoryDetailsById")]
        public async Task<IActionResult> GetInventoryDetailsById(Guid inventoryId) =>
           Ok(await _inventoryService.GetInventoryDetailsById(inventoryId).ConfigureAwait(true));
    }
}
