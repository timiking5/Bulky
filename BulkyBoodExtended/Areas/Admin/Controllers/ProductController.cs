using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BulkyBookExtended.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    public ActionResult Index()
    {
        List<Product> productList = _unitOfWork.Product.GetAll().ToList();
        
        return View(productList);
    }
    public IActionResult UpSert(int? id)  // Update + Insert
    {
        IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        ViewData["CategoryList"] = categoryList;
        if (id == 0 || id == null)
        {
            return View(new Product { Id = 0 });
        }
        // update
        Product product = _unitOfWork.Product.Get(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
    [HttpPost]
    public IActionResult UpSert(Product product, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = 
                        Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                product.ImageUrl = @"\images\product\" + fileName;
            }
            if (product.Id == 0)
            {
                _unitOfWork.Product.Create(product);
                TempData["success"] = "Product created successfully";
            }
            else
            {
                _unitOfWork.Product.Update(product);
                TempData["success"] = "Product updated successfully";
            }
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
        Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Delete")]

    public IActionResult DeletePost(int? id)
    {
        Product product = _unitOfWork.Product.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        _unitOfWork.Product.Delete(product);
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }
}
