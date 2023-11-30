using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class ContactService : IContactService
    {
        private IUnitofWork _unitofWork;
        public ContactService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void DeleteContact(int id)
        {
            var model = _unitofWork.GenericRepository<Contact>().GetById(id);
            _unitofWork.GenericRepository<Contact>().Delete(model);
            _unitofWork.Save();
        }

        public PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ContactViewModel();
            int totalCount;
            List<ContactViewModel> vmList = new List<ContactViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitofWork.GenericRepository<Contact>().GetAll(includeProperties:"Hospital").Skip(ExcludeRecords).Take(pageSize).ToList();
                totalCount = _unitofWork.GenericRepository<Contact>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ContactViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public ContactViewModel GetContactById(int contactId)
        {
            var model = _unitofWork.GenericRepository<Contact>().GetById(contactId);
            var vm = new ContactViewModel(model);
            return vm;
        }

        public void InsertContact(ContactViewModel contact)
        {
            var model = new ContactViewModel().ConvertViewModel(contact);
            _unitofWork.GenericRepository<Contact>().Add(model);
            _unitofWork.Save();
        }

        public void UpdateContact(ContactViewModel contact)
        {
            var model = new ContactViewModel().ConvertViewModel(contact);
            var ModelById = _unitofWork.GenericRepository<Contact>().GetById(model.Id);
            ModelById.Phone = contact.Phone;
            ModelById.Email = contact.Email;
            ModelById.HospitalId = contact.HospitalInfoId;
            _unitofWork.GenericRepository<Contact>().Update(ModelById);
            _unitofWork.Save();
        }
        private List<ContactViewModel> ConvertModelToViewModelList(List<Contact> modelList)
        {
            return modelList.Select(x => new ContactViewModel(x)).ToList();
        }
    }
}
