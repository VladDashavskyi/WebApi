using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApi.Dal.Context;
using WebApi.Dal.Entities;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class AccountService: IAccountService
    {
        private readonly ProjectContext _appContext;
        private readonly IMapper _mapper;
        public AccountService(ProjectContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
        {

            return await _appContext.Accounts.Include(i => i.Incident).ProjectTo<AccountDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AccountDto> GetAccountByIdAsync(int accountId)
        {
            var account = await _appContext.Accounts.Where(w => w.AccountID == accountId)
                .Include(i => i.Incident).FirstOrDefaultAsync();

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> CreateAccountAsync(BaseAccountDto accountDto)
        {
            if (!_appContext.Contacts.Any(w => w.ContactID == accountDto.ContactID))
            {
                throw new ArgumentException("Contact does not exist");
            }

            if (_appContext.Accounts.Any(w => w.Name == accountDto.Name))
            {
                throw new ArgumentException($"Account with name {accountDto.Name} exist");
            }

            var account = new Account
            {
                Name = accountDto.Name,
                ContactID = accountDto.ContactID,
            };

            _appContext.Accounts.Add(account);
            await _appContext.SaveChangesAsync();

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> UpdateAccountAsync(int accountId, BaseAccountDto accountDto)
        {
            var account = await _appContext.Accounts.Where(w => w.AccountID == accountId).FirstOrDefaultAsync();
            if (account != null)
            {
                account.Name = accountDto.Name;
                account.ContactID = accountDto.ContactID;
            }
            await _appContext.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);

        }

        public async Task DeleteAccountAsync(int accountId)
        {
            var account = await _appContext.Accounts.Where(w => w.AccountID == accountId).FirstOrDefaultAsync();
            if (account != null)
            {
                try
                {
                    _appContext.BeginTransaction();
                    _appContext.Accounts.Remove(account);
                    _appContext.Commit();
                    await _appContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _appContext.Rollback();
                }
            }
        }
    }
}
