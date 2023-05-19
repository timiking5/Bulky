using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Category category { get; set; }
        private readonly ApplicationDBContext _dbContext;
        public EditModel(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            category = _dbContext.Categories.Find(id);
        }
        public IActionResult OnPost()
        {
            _dbContext.Update(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }
    }
}
