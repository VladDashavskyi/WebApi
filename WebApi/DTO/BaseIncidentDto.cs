using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class BaseIncidentDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int AccountID { get; set; }

    }
}


