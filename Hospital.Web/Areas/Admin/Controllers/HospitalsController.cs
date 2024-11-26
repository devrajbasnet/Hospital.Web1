using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HospitalsController : Controller
    {
        private readonly IHospitalInfo _hospitalInfo;
        public HospitalsController(IHospitalInfo hospitalInfo)
        {
            _hospitalInfo = hospitalInfo;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_hospitalInfo.GetAll(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _hospitalInfo.GetHospitalById(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HospitalInfoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _hospitalInfo.UpdateHospitalInfo(vm);
                return RedirectToAction("Index");
            }
            return View(vm);// Return the form with validation errors if invalid
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HospitalInfoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _hospitalInfo.InsertHospitalInfo(vm);
                return RedirectToAction("Index");
            }
            return View(vm); // Return the form with validation error
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var hospital = _hospitalInfo.GetHospitalById(id);
            if (hospital == null)
            {
                return NotFound();
            }
            _hospitalInfo.DeleteHospitalInfo(id);
            return RedirectToAction("Index");
        }
    }
}
