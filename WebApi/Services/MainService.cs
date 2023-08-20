using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System;
using System.Data;
using WebApi.Dal.Context;
using WebApi.DTO;
using WebApi.Interfaces;
using WebApi.Dal.Entities;
using System.Net;

namespace WebApi.Services
{
    public class MainService : IMainService
    {
        private readonly ProjectContext _appContext;
        private readonly IMapper _mapper;
        public MainService(ProjectContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;

        }
        public async Task<ContactDto> GetContactByIdAsync(int contactId)
        {
            if (!_appContext.Contacts.Any(w => w.ContactID == contactId))
            {
                throw new ("Account does not exist");
            }
            var contact = await _appContext.Contacts.Where(w => w.ContactID == contactId)
                .Include(i => i.Account)
                .FirstOrDefaultAsync();

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<IEnumerable<ContactDto>> GetContactsAsync()
        {
            return await _appContext.Contacts.Include(i => i.Account).ProjectTo<ContactDto>(_mapper.ConfigurationProvider).ToListAsync();                    
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

        public async Task<IEnumerable<IncidentDto>> GetIncidentsAsync()
        {

            return await _appContext.Incidents.ProjectTo<IncidentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IncidentDto> GetIncidentByNameAsync(string incidentName)
        {
            var incident = await _appContext.Incidents.Where(w => w.IncidentName == incidentName).FirstOrDefaultAsync();

            return _mapper.Map<IncidentDto>(incident);
        }

        public async Task<ContactDto> CreateContactAsync(BaseContactDto contactDto)
        {
            if (_appContext.Contacts.Any(w => w.Email == contactDto.Email))
            {
                throw new ArgumentException($"Contact with {contactDto.Email} exist");
            }

            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
            };
            _appContext.Contacts.Add(contact);
            await _appContext.SaveChangesAsync();

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> UpdateContactAsync(int contactId, BaseContactDto contactDto)
        {
            if (_appContext.Contacts.Any(w => w.Email == contactDto.Email))
            {
                throw new ArgumentException($"Contact with {contactDto.Email} exist");
            }

            var contact = await _appContext.Contacts.Where(w => w.ContactID == contactId).FirstOrDefaultAsync();
            if (contact != null)
            {
                contact.FirstName = contactDto.FirstName;
                contact.LastName = contactDto.LastName;
                contact.Email = contactDto.Email;
            }
            await _appContext.SaveChangesAsync();
            return _mapper.Map<ContactDto>(contact);

        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await _appContext.Contacts.Where(w => w.ContactID == contactId).FirstOrDefaultAsync();
            if (contact != null)
            {
                try
                {
                    _appContext.BeginTransaction();
                    _appContext.Contacts.Remove(contact);
                    _appContext.Commit();
                    await _appContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _appContext.Rollback();
                }
            }
        }

        #region Account
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
        #endregion

        #region Incident

        public async Task<IncidentDto> CreateIncidentAsync(BaseIncidentDto incidentDto)
        {
            var incident = new Incident
            {
                IncidentName = Guid.NewGuid().ToString(),
                Description = incidentDto.Description,
                AccountID = incidentDto.AccountID,
            };
            _appContext.Incidents.Add(incident);
            await _appContext.SaveChangesAsync();

            return _mapper.Map<IncidentDto>(incident);
        }

        public async Task<IncidentDto> UpdateIncidentAsync(string incidentName, BaseIncidentDto incidentDto)
        {
            var incident = await _appContext.Incidents.Where(w => w.IncidentName == incidentName).FirstOrDefaultAsync();
            if (incident != null)
            {
                incident.Description = incidentDto.Description;
                incident.AccountID = incidentDto.AccountID;
            }
            await _appContext.SaveChangesAsync();
            return _mapper.Map<IncidentDto>(incident);

        }

        public async Task DeleteIncidentAsync(string incidentName)
        {
            var incident = await _appContext.Incidents.Where(w => w.IncidentName == incidentName).FirstOrDefaultAsync();
            if (incident != null)
            {
                try
                {
                    _appContext.BeginTransaction();
                    _appContext.Incidents.Remove(incident);
                    _appContext.Commit();
                    await _appContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _appContext.Rollback();
                }
            }
        }

        public async Task<IncidentDto> CreateIncidentByUserAsync(IncidentByUserDto incidentDto)
        {
            try
            {
                var contact = _appContext.Contacts.FirstOrDefault(w => w.Email == incidentDto.Email);

                if (contact != null)
                {
                    throw new ArgumentException($"Contact with {incidentDto.Email} exist");
                }

                contact = new Contact
                {
                    FirstName = incidentDto.FirstName,
                    LastName = incidentDto.LastName,
                    Email = incidentDto.Email,                    
                };

                _appContext.Contacts.Add(contact);
                _appContext.SaveChanges();

                var account = _appContext.Accounts.FirstOrDefault(w => w.Name == incidentDto.AccountName);

                if (account == null)
                {
                    account = new Account
                    {
                        Name = incidentDto.AccountName,
                        ContactID = contact.ContactID,
                    };

                    _appContext.Accounts.Add(account);
                    _appContext.SaveChanges();

                }
                else
                {
                    account.ContactID = contact.ContactID;
                    _appContext.Accounts.Update(account);
                }

                var incident = new Incident
                {
                    IncidentName = Guid.NewGuid().ToString(),
                    AccountID = account.AccountID,
                    Description = incidentDto.IncidentDescription
                };

                _appContext.Incidents.Add(incident);

                await _appContext.SaveChangesAsync();

                return _mapper.Map<IncidentDto>(incident);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Incident does not create");
            }            
        }

        #endregion
    }
}
