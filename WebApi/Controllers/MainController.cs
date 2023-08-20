using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Net.Mail;
using WebApi.Dal.Context;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("controller")]
    [ApiController]
    public class MainController : ControllerBase
    {

        ProjectContext db;
        private readonly ILogger<MainController> _logger;
        private readonly IMainService _mainService;

        public MainController(ProjectContext context, ILogger<MainController> logger, IMainService mainServices)
        {
            _logger = logger;
            db = context;
            _mainService = mainServices;
        }

        [HttpGet]
        [Route("/Contact{contactId}")]
        public async Task<ActionResult<ContactDto>> GetContactById(int contactId)
        {
            
            return await _mainService.GetContactByIdAsync(contactId);
            
        }

        [HttpGet]
        [Route("/Contacts")]
        public async Task<IEnumerable<BaseContactDto>> GetContacts()
        {
            return await _mainService.GetContactsAsync();

        }

        [HttpGet]
        [Route("/Account{accountId}")]
        public async Task<ActionResult<AccountDto>> GetAccountById(int accountId)
        {
            return await _mainService.GetAccountByIdAsync(accountId);

        }

        [HttpGet]
        [Route("/Accounts")]
        public async Task<IEnumerable<AccountDto>> GetAccounts()
        {
            return await _mainService.GetAccountsAsync();

        }

        [HttpGet]
        [Route("/Incident{incidentName}")]
        public async Task<ActionResult<IncidentDto>> GetIncidentByNameAsync(string incidentName)
        {
            return await _mainService.GetIncidentByNameAsync(incidentName);

        }

        [HttpGet]
        [Route("/Incidents")]
        public async Task<IEnumerable<IncidentDto>> GetIncidentsAsync()
        {
            return await _mainService.GetIncidentsAsync();

        }

        [HttpPost]
        [Route("/Contact")]
        public async Task<ActionResult<BaseContactDto>> CreateContactAsync(BaseContactDto contact)
        {
            try
            {
                return await _mainService.CreateContactAsync(contact);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }

        }

        [HttpPut]
        [Route("/Contact{contactId}")]
        public async Task<ActionResult<BaseContactDto>> UpdateContactAsync(int contactId, BaseContactDto contact)
        {
            try
            {
                return await _mainService.UpdateContactAsync(contactId, contact);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }

        }

        [HttpDelete]
        [Route("/Contact{contactId}")]
        public async Task DeleteContactAsync(int contactId)
        {
            await _mainService.DeleteContactAsync(contactId);
        }


        [HttpPost]
        [Route("/Account")]
        public async Task<ActionResult<AccountDto>> CreateAccountAsync(BaseAccountDto account)
        {
            try
            {
                return await _mainService.CreateAccountAsync(account);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");

            }
        }

        [HttpPut]
        [Route("/Account{accountId}")]
        public async Task<ActionResult<AccountDto>> UpdateAccountAsync(int accountId, BaseAccountDto account)
        {
            return await _mainService.UpdateAccountAsync(accountId, account);

        }

        [HttpDelete]
        [Route("/Account{accountId}")]
        public async Task<ActionResult> DeleteAccountAsync(int accountId)
        {
            try
            {
                await _mainService.DeleteAccountAsync(accountId);
                return Ok();
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok();
            }
        }

        [HttpPost]
        [Route("/Incident")]
        public async Task<ActionResult<IncidentDto>> CreateIncidentAsync(BaseIncidentDto incident)
        {
            return await _mainService.CreateIncidentAsync(incident);
        }

        [HttpPut]
        [Route("/Incident{incidentName}")]
        public async Task<ActionResult<IncidentDto>> UpdateIncidentAsync(string incidentName, BaseIncidentDto incident)
        {
            return await _mainService.UpdateIncidentAsync(incidentName, incident);

        }

        [HttpDelete]
        [Route("/Incident{incidentName}")]
        public async Task DeleteIncidentAsync(string incidentName)
        {
            await _mainService.DeleteIncidentAsync(incidentName);
        }

        [HttpPost]
        [Route("/CreateIncident")]
        public async Task<ActionResult<IncidentDto>> CreateIncidentByUserAsync(IncidentByUserDto incident)
        {
            try { 
            return await _mainService.CreateIncidentByUserAsync(incident);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }
        }
    }
}
