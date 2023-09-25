using CarTypeService.Models;
using CarTypeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarTypeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorApiController : ControllerBase
    {

        private readonly IMotorApiService _motorApiService;

        private readonly ILogger<MotorApiController> _logger;

        public MotorApiController(ILogger<MotorApiController> logger, IMotorApiService motorApiService)
        {
            _logger = logger;
            _motorApiService = motorApiService;
        }

        [HttpGet("/vehicles/{licensePlate}")]
        public async Task<CarDescription> Get(string licensePlate)
        {
            return await _motorApiService.GetDescriptionAsync(licensePlate);
         
        }     
    }
}