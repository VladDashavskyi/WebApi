using WebApi.DTO;

namespace WebApi.Interfaces
{
    public interface IContactService
    {

        ContactDto GetContactById(int contactId);
        IEnumerable<ContactDto> GetContacts();

        ContactDto CreateContact(BaseContactDto contactDto);
        ContactDto UpdateContact(int contactId, BaseContactDto userDto);
        void DeleteContact(int contactId);
    }
}
