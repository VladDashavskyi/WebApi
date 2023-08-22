using WebApi.DTO;

namespace WebApi.Interfaces
{
    public interface IAccountService
    {

        Task<IEnumerable<AccountDto>> GetAccountsAsync();
        Task<AccountDto> GetAccountByIdAsync(int accountId);

        Task<AccountDto> CreateAccountAsync(BaseAccountDto accountDto);
        Task<AccountDto> UpdateAccountAsync(int accountId, BaseAccountDto accountDto);
        Task DeleteAccountAsync(int accountId);

    }
}
