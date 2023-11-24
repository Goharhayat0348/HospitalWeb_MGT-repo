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
    public class DoctorService : IDoctorService
    {
        private IUnitofWork _unitofWork;
        public DoctorService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void DeleteTiming(int timingid)
        {
            var model = _unitofWork.GenericRepository<Timing>().GetById(timingid);
            _unitofWork.GenericRepository<Timing>().Delete(model);
            _unitofWork.Save();
        }

        public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new TimingViewModel();
            int totalCount;
            List<TimingViewModel> vmList = new List<TimingViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitofWork.GenericRepository<Timing>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitofWork.GenericRepository<Timing>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);

            }
            catch (Exception)
            {

                throw;
            }

            var result = new PagedResult<TimingViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<TimingViewModel> ConvertModelToViewModelList(List<Timing> modelList)
        {
            return modelList.Select(x => new TimingViewModel(x)).ToList();
        }

        public IEnumerable<TimingViewModel> GetAll()
        {
            var TimingList = _unitofWork.GenericRepository<Timing>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(TimingList);
            return vmList;
        }

        public TimingViewModel GetTimingById(int TimingId)
        {
            var model = _unitofWork.GenericRepository<Timing>().GetById(TimingId);
            var vm = new TimingViewModel(model);
            return vm;
        }

        public void InsertTiming(TimingViewModel timing)
        {
            var model = new TimingViewModel().ConvertViewModel(timing);
            _unitofWork.GenericRepository<Timing>().Add(model);
            _unitofWork.Save();
        }

        public void UpdateTiming(TimingViewModel timing)
        {
            var model = new TimingViewModel().ConvertViewModel(timing);
            var ModelById = _unitofWork.GenericRepository<Timing>().GetById(model.Id);
            ModelById.Id = timing.Id;
            ModelById.Doctor = timing.Doctor;
            ModelById.Status = timing.Status;
            ModelById.Duration = timing.Duration;
            ModelById.MorningShiftStartTime = timing.MorningShiftStartTime;
            ModelById.MorningShiftEndTime = timing.MorningShiftEndTime;
            ModelById.AfternoonShiftStartTime = timing.AfternoonShiftStartTime;
            ModelById.AfternoonShiftEndTime = timing.AfternoonShiftEndTime;

            _unitofWork.GenericRepository<Timing>().Update(ModelById);
            _unitofWork.Save();
        }
    }
}
