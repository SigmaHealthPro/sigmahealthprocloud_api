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
    public class EventController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<EventController> _logger;
        public EventController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<EventController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;

        }
        [HttpPost]
        [Route("createEvent")]
        public async Task<IActionResult> InsertEvent(EventModel model) =>
            Ok(await _unitOfWork.Events.InsertAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("searchevent")]
        public async Task<IActionResult> SearchEvent(SearchEventParams model) =>
          Ok(await _unitOfWork.Events.GetAllAsync(model).ConfigureAwait(true));

        [HttpGet]
        [Route("Events")]
        public async Task<IActionResult> GetAllEvents()
           => Ok(await _unitOfWork.Events.GetAllEvents().ConfigureAwait(true));


    }
}
