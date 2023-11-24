using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class ApplicationUserViewModel
    {
       
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public bool IsDoctor { get; set; }
        public string Specialist { get; set; }


        public ApplicationUserViewModel()
        {

        }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            Name= user.Name;
            City = user.City;
            Gender= user.Gender;
            IsDoctor= user.IsDoctor;
            Specialist = user.Specialist;
            Email= user.Email;
            UserName= user.UserName;
        }
        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel userViewModel)
        {
            return new ApplicationUser
            {
                Name=userViewModel.Name,
                City=userViewModel.City,
                Gender=userViewModel.Gender,
                IsDoctor= userViewModel.IsDoctor,
                Specialist=userViewModel.Specialist,
                Email= userViewModel.Email,
                UserName= userViewModel.UserName
            };
        }
        public List<ApplicationUser> Doctor { get; set; } = new List<ApplicationUser>();
    }
}
