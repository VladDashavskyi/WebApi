using Microsoft.AspNetCore.Mvc;
using WebApi.Dal.Context;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("ContactController")]
    [ApiController]

    public class IncidentController : Controller
    {

        ProjectContext db;
        private readonly ILogger<IncidentController> _logger;
        private readonly IIncidentService _incidentService;

        public IncidentController(ProjectContext context, ILogger<IncidentController> logger, IIncidentService incidentServices)
        {
            _logger = logger;
            db = context;
            _incidentService = incidentServices;
        }

        [HttpGet]
        [Route("/Incident{incidentName}")]
        public async Task<ActionResult<IncidentDto>> GetIncidentByNameAsync(string incidentName)
        {
            return await _incidentService.GetIncidentByNameAsync(incidentName);

        }

        [HttpGet]
        [Route("/Incidents")]
        public async Task<IEnumerable<IncidentDto>> GetIncidentsAsync()
        {
            return await _incidentService.GetIncidentsAsync();

        }
        [HttpPost]
        [Route("/Incident")]
        public async Task<ActionResult<IncidentDto>> CreateIncidentAsync(BaseIncidentDto incident)
        {
            return await _incidentService.CreateIncidentAsync(incident);
        }

        [HttpPut]
        [Route("/Incident{incidentName}")]
        public async Task<ActionResult<IncidentDto>> UpdateIncidentAsync(string incidentName, BaseIncidentDto incident)
        {
            return await _incidentService.UpdateIncidentAsync(incidentName, incident);

        }

        [HttpDelete]
        [Route("/Incident{incidentName}")]
        public async Task DeleteIncidentAsync(string incidentName)
        {
            await _incidentService.DeleteIncidentAsync(incidentName);
        }

        [HttpPost]
        [Route("/CreateIncident")]
        public async Task<ActionResult<IncidentDto>> CreateIncidentByUserAsync(IncidentByUserDto incident)
        {
            try
            {
                return await _incidentService.CreateIncidentByUserAsync(incident);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }
        }

    }
}
