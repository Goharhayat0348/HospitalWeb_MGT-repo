using Hospital.Utilities;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IRoomService
    {
        PagedResult<RoomViewModel> GetAll(int pageNumber,int pageSize);
        RoomViewModel GetRoomById(int RoomId);
        void UpdateRoom(RoomViewModel Room);
        void InsertRoom(RoomViewModel Room);
        void DeleteRoom(int id);
    }
}
