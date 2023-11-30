using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HospitalWebMGT.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_doctorService.GetAll(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult AddTiming()
        {
            Timing timing = new Timing();
            List<SelectListItem> morningShiftStart = new List<SelectListItem>();
            List<SelectListItem> morningShiftEnd = new List<SelectListItem>();
            List<SelectListItem> afternoonShiftStart = new List<SelectListItem>();
            List<SelectListItem> afternoonShiftEnd = new List<SelectListItem>();

            for (int i = 1; i <= 11; i++)
            {
                morningShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            for (int i = 1; i <= 13; i++)
            {
                morningShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            for (int i = 13; i <= 16; i++)
            {
                afternoonShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            for (int i = 13; i <= 18; i++)
            {
                afternoonShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            ViewBag.morningStart = new SelectList(morningShiftStart, "Value", "Text");
            ViewBag.morningEnd = new SelectList(morningShiftEnd, "Value", "Text");
            ViewBag.evenStart = new SelectList(afternoonShiftStart, "Value", "Text");
            ViewBag.evenEnd = new SelectList(afternoonShiftEnd, "Value", "Text");

            TimingViewModel vm = new TimingViewModel();
            vm.ScheduleDate = DateTime.Now;
            vm.ScheduleDate = vm.ScheduleDate.AddDays(1);
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddTiming(TimingViewModel vm)
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (Claims != null)
            {
                vm.Doctor.Id = Claims.Value;
                _doctorService.InsertTiming(vm);             
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _doctorService.GetTimingById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(TimingViewModel vm)
        {
            _doctorService.UpdateTiming(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TimingViewModel vm)
        {
            _doctorService.InsertTiming(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _doctorService.DeleteTiming(id);
            return RedirectToAction("Index");
        }
    }
}
