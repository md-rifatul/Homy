using Homy.Domain.Entities;
using Homy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homy.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var villa = await _db.Villas.ToListAsync();
            return View(villa);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("", "The description cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The vill has been created successfully";
                return RedirectToAction("Index");
            }
            return View();
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
