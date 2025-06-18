using HospitalAppointmentBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentBooking.WebApi.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentQueryService _queryService;
        public AppointmentsController(IAppointmentQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_queryService.GetAllDetails());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_queryService.GetDetailById(id));
        }
    }
}
