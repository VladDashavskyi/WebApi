using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApi.Dal.Context;
using WebApi.Dal.Entities;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class IncidentService: IIncidentService
    {
        private readonly ProjectContext _appContext;
        private readonly IMapper _mapper;
        public IncidentService(ProjectContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;

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
    }
}
