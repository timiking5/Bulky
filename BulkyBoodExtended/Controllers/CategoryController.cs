using BulkyBoodExtended.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBoodExtended.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext db)
        {

            _dbContext = db;

        }
        public IActionResult Index()
        {
            var categoryList = _dbContext.Categories.ToList();
            return View(categoryList);
        }
    }
}
