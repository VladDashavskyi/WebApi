using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApi.Dal.Context;
using WebApi.Dal.Entities;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class ContactService: IContactService
    {
        //private readonly ProjectContext _appContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IMapper mapper, UnitOfWork unitOfWork)
        {
            
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public ContactDto GetContactById(int contactId)
        {
            if (_unitOfWork.ContactRepository.GetByID(contactId) == null)
            {
                throw new("Account does not exist");
            }
            var contact = _unitOfWork.ContactRepository.Get(filter => filter.ContactID == contactId, null, "Account").FirstOrDefault();

            return _mapper.Map<ContactDto>(contact);
        }

        public IEnumerable<ContactDto> GetContacts()
        {
            var contacts =  _unitOfWork.ContactRepository.Get(null, null, "Account");

            return _mapper.Map<List<ContactDto>>(contacts);
        }

        public ContactDto CreateContact(BaseContactDto contactDto)
        {
            if (_unitOfWork.ContactRepository.Get(filter => filter.Email == contactDto.Email).Any())
            {
                throw new ArgumentException($"Contact with {contactDto.Email} exist");
            }

            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
            };
            _unitOfWork.ContactRepository.Insert(contact);
            _unitOfWork.Save();

            return _mapper.Map<ContactDto>(contact);
        }

        public ContactDto UpdateContact(int contactId, BaseContactDto contactDto)
        {
            if (_unitOfWork.ContactRepository.Get(filter => filter.Email == contactDto.Email).Any())
            {
                throw new ArgumentException($"Contact with {contactDto.Email} exist");
            }

            var contact = _unitOfWork.ContactRepository.GetByID(contactId);
            if (contact != null)
            {
                contact.FirstName = contactDto.FirstName;
                contact.LastName = contactDto.LastName;
                contact.Email = contactDto.Email;
            }
            _unitOfWork.ContactRepository.Update(contact);
            _unitOfWork.Save();
            return _mapper.Map<ContactDto>(contact);

        }

        public void DeleteContact(int contactId)
        {
            var contact = _unitOfWork.ContactRepository.GetByID(contactId);

            if (contact == null)
            {
                throw new("Account does not exist");
            }
            else
            {
                try
                {

                    _unitOfWork.ContactRepository.Delete(contact);

                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Contact wasn't deleted, {ex.Message}");
                }
            }
        }

    }
}
