using ASPNET_Project.Data;
using ASPNET_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ASPNET_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> Categories = _db.Categories;
            return View(Categories);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if(category == null)
            {
                return NotFound();

            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }


        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var del = _db.Categories.Find(id);
            if(del == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(del);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }   



    }
}
