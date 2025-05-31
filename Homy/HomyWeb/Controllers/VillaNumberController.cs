using Homy.Domain.Entities;
using Homy.Infrastructure.Data;
using Homy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homy.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var villaNumber = await _db.villaNumbers.Include(u => u.Villa).ToListAsync();
            return View(villaNumber);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            bool roonNumberExists = _db.villaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

            ModelState.Remove("Villa");
            if (ModelState.IsValid && !roonNumberExists)
            {
                _db.villaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The vill number has been created successfully";
                return RedirectToAction("Index");
            }
            if (roonNumberExists)
            {
                TempData["error"] = "The villa Number already exists";
            }

            obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);
        }
        public IActionResult Update(int VillaId)
        {
            var villa = _db.Villas.FirstOrDefault(u => u.Id == VillaId);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The vill has been update successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The villa could not been update";
            return View();
        }
        public IActionResult Remove(int VillaId)
        {
            var villa = _db.Villas.FirstOrDefault(u => u.Id == VillaId);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Remove(Villa obj)
        {
            var villa = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
            if (villa != null)
            {
                _db.Villas.Remove(villa);
                _db.SaveChanges();
                TempData["success"] = "The vill has been deleted successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The villa could not been deleted";
            return RedirectToAction("Error", "Home");

        }
    }
}
