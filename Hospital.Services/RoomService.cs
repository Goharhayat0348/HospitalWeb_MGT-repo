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
    public class RoomService:IRoomService
    {
        private IUnitofWork _unitofWork;
        public RoomService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

       

        public void DeleteRoom(int id)
        {
            var model = _unitofWork.GenericRepository<Room>().GetById(id);
            _unitofWork.GenericRepository<Room>().Delete(model);
            _unitofWork.Save();
        }

        public PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new RoomViewModel();
            int totalCount;
            List<RoomViewModel> vmList = new List<RoomViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitofWork.GenericRepository<Room>().GetAll(includeProperties:"Hospital")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitofWork.GenericRepository<Room>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);

            }
            catch (Exception)
            {

                throw;
            }

            var result = new PagedResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        

        public RoomViewModel GetRoomById(int RoomId)
        {
            var model = _unitofWork.GenericRepository<Room>().GetById(RoomId);
            var vm = new RoomViewModel(model);
            return vm;
        }

       

        public void InsertRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            _unitofWork.GenericRepository<Room>().Add(model);
            _unitofWork.Save();
        }

       

        public void UpdateRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            var ModelById = _unitofWork.GenericRepository<Room>().GetById(model.Id);
            ModelById.Type = Room.Type;
            ModelById.RoomNumber = Room.RoomNumber;
            ModelById.Status = Room.Status;
            ModelById.HospitalId = Room.HospitalInfoId;
           
            _unitofWork.GenericRepository<Room>().Update(ModelById);
            _unitofWork.Save();
        }

        private List<RoomViewModel> ConvertModelToViewModelList(List<Room> modelList)
        {
            return modelList.Select(x => new RoomViewModel(x)).ToList();
        }
    }
}
