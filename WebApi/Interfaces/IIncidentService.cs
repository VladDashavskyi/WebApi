using WebApi.DTO;

namespace WebApi.Interfaces
{
    public interface IIncidentService
    {

        Task<IncidentDto> GetIncidentByNameAsync(string incidentName);
        Task<IEnumerable<IncidentDto>> GetIncidentsAsync();
        Task<IncidentDto> CreateIncidentAsync(BaseIncidentDto incidentDto);
        Task<IncidentDto> UpdateIncidentAsync(string incidentName, BaseIncidentDto incidentDto);
        Task DeleteIncidentAsync(string incidentName);
        Task<IncidentDto> CreateIncidentByUserAsync(IncidentByUserDto incidentDto);
    }
}
