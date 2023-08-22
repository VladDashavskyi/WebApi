using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Dal.Context;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("AccountController")]
    [ApiController]
    public class AccountController : Controller
    {

        ProjectContext db;
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ProjectContext context, ILogger<AccountController> logger, IAccountService incidentServices)
        {
            _logger = logger;
            db = context;
            _accountService = incidentServices;
        }

        [HttpGet]
        [Route("/Account{accountId}")]
        public async Task<ActionResult<AccountDto>> GetAccountById(int accountId)
        {
            return await _accountService.GetAccountByIdAsync(accountId);

        }

        [HttpGet]
        [Route("/Accounts")]
        public async Task<IEnumerable<AccountDto>> GetAccounts()
        {
            return await _accountService.GetAccountsAsync();

        }

        [HttpPost]
        [Route("/Account")]
        public async Task<ActionResult<AccountDto>> CreateAccountAsync(BaseAccountDto account)
        {
            try
            {
                return await _accountService.CreateAccountAsync(account);
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
            return await _accountService.UpdateAccountAsync(accountId, account);

        }

        [HttpDelete]
        [Route("/Account{accountId}")]
        public async Task<ActionResult> DeleteAccountAsync(int accountId)
        {
            try
            {
                await _accountService.DeleteAccountAsync(accountId);
                return Ok();
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok();
            }
        }
    }
}
