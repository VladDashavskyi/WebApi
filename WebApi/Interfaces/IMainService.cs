using WebApi.DTO;

namespace WebApi.Interfaces
{
    public interface IMainService
    {
        Task<ContactDto> GetContactByIdAsync(int contactId);
        Task<IEnumerable<ContactDto>> GetContactsAsync();
        Task<IEnumerable<AccountDto>> GetAccountsAsync();
        Task<AccountDto> GetAccountByIdAsync(int accountId);
        Task<IncidentDto> GetIncidentByNameAsync(string incidentName);
        Task<IEnumerable<IncidentDto>> GetIncidentsAsync();
        Task<ContactDto> CreateContactAsync(BaseContactDto contactDto);
        Task<ContactDto> UpdateContactAsync(int contactId, BaseContactDto userDto);
        Task DeleteContactAsync(int contactId);

        Task<AccountDto> CreateAccountAsync(BaseAccountDto accountDto);
        Task<AccountDto> UpdateAccountAsync(int accountId, BaseAccountDto accountDto);
        Task DeleteAccountAsync(int accountId);

        Task<IncidentDto> CreateIncidentAsync(BaseIncidentDto incidentDto);
        Task<IncidentDto> UpdateIncidentAsync(string incidentName, BaseIncidentDto incidentDto);
        Task DeleteIncidentAsync(string incidentName);
        Task<IncidentDto> CreateIncidentByUserAsync(IncidentByUserDto incidentDto);
    }

}
