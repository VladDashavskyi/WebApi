using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{

    public class BaseAccountDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }
        [Required]
        public int ContactID { get; set; }

    }

}
