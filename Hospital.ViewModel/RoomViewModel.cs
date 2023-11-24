using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class RoomViewModel
    {

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int HospitalInfoId { get; set; }
        public HospitalInfo HospitalInfo { get; set; }


        public RoomViewModel()
        {

        }

        public RoomViewModel(Room model)
        {
            Id = model.Id;
            RoomNumber = model.RoomNumber;
            Type = model.Type;
            Status = model.Status;
            HospitalInfoId = model.HospitalId;
            HospitalInfo = model.Hospital;
        }

        public Room ConvertViewModel(RoomViewModel model)
        {
            return new Room
            {
                Id = model.Id,
                Type = model.Type,
                RoomNumber = model.RoomNumber,
                Status = model.Status,
                HospitalId = model.HospitalInfoId,
                Hospital = model.HospitalInfo
            };
        }
    }
}
