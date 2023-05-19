using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category category { get; set; }
        private readonly ApplicationDBContext _db;
        public CreateModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
