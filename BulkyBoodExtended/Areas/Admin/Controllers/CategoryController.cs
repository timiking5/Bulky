using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookExtended.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository CategoryRepo;
        public CategoryController(ICategoryRepository repo)
        {
            CategoryRepo = repo;
        }
        public IActionResult Index()
        {
            var categoryList = CategoryRepo.GetAll();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order cannot exactly match name");
            }
            if (ModelState.IsValid)
            {
                CategoryRepo.Create(model);
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = CategoryRepo.Get(x => x.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                CategoryRepo.Update(model);
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = CategoryRepo.Get(x => x.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDb = CategoryRepo.Get(x => x.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            CategoryRepo.Delete(categoryFromDb);
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
