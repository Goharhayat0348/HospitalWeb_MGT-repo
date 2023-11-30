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
    public class HospitalInfoService : IHospitalInfo
    {
        private IUnitofWork _unitofWork;
        public HospitalInfoService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void DeleteHospitalInfo(int id)
        {
            var model = _unitofWork.GenericRepository<HospitalInfo>().GetById(id);
            _unitofWork.GenericRepository<HospitalInfo>().Delete(model);
            _unitofWork.Save();
        }

        public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitofWork.GenericRepository<HospitalInfo>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();
                totalCount = _unitofWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public HospitalInfoViewModel GetHospitalById(int HospitalId)
        {
            var model = _unitofWork.GenericRepository<HospitalInfo>().GetById(HospitalId);
            var vm = new HospitalInfoViewModel(model);
            return vm;
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitofWork.GenericRepository<HospitalInfo>().Add(model);
            _unitofWork.Save();
        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            var ModelById = _unitofWork.GenericRepository<HospitalInfo>().GetById(model.Id);
            ModelById.Name = hospitalInfo.Name;
            ModelById.City = hospitalInfo.City;
            ModelById.PinCode = hospitalInfo.PinCode;
            ModelById.Country = hospitalInfo.Country;
            _unitofWork.GenericRepository<HospitalInfo>().Update(ModelById);
            _unitofWork.Save();
        }

        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
        {
            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
        }
        public IEnumerable<HospitalInfoViewModel> GetAll()
        {
            var TimingList = _unitofWork.GenericRepository<HospitalInfo>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(TimingList);
            return vmList;
        }
    }
}
