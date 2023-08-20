namespace WebApi.DTO
{
    public class ContactDto : BaseContactDto
    {
        public int ContactId { get; set; }
        public AccountDto Account {get;set;}
    }
}
