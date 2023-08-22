using Microsoft.AspNetCore.Mvc;
using WebApi.Dal.Context;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{

    [Route("ContactController")]
    [ApiController]



    public class ContactController : Controller
    {

        ProjectContext db;
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ProjectContext context, ILogger<ContactController> logger, IContactService contactServices)
        {
            _logger = logger;
            db = context;
            _contactService = contactServices;
        }

        [HttpGet]
        [Route("/Contact{contactId}")]
        public ActionResult<ContactDto> GetContactById(int contactId)
        {

            return _contactService.GetContactById(contactId);

        }

        [HttpGet]
        [Route("/Contacts")]
        public IEnumerable<BaseContactDto> GetContacts()
        {
            return  _contactService.GetContacts();

        }



        [HttpPost]
        [Route("/Contact")]
        public ActionResult<BaseContactDto> CreateContact(BaseContactDto contact)
        {
            try
            {
                return _contactService.CreateContact(contact);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }

        }

        [HttpPut]
        [Route("/Contact{contactId}")]
        public ActionResult<BaseContactDto> UpdateContact(int contactId, BaseContactDto contact)
        {
            try
            {
                return _contactService.UpdateContact(contactId, contact);
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }

        }

        [HttpDelete]
        [Route("/Contact{contactId}")]
        public ActionResult DeleteContact(int contactId)
        {
            try
            {
                _contactService.DeleteContact(contactId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"message {ex.Message}");
            }
        }

    }
}
