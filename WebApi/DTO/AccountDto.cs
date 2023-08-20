namespace WebApi.DTO
{

    public class AccountDto: BaseAccountDto
    {
        public int AccountID { get; set; }
        public IncidentDto Incident { get; set; }
    }

}
