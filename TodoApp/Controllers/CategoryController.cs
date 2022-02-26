using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class CategoryController : Controller
    {

        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET ALL CATEGORIES
        public IActionResult Create()
        {
            return View();
        }        
        
        //POST NEW CATEGORY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                // key, value - key musi być unikalny
                ModelState.AddModelError("DispErrMatchError", "Display Order cannot match the Name of category");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                // można dodać drugi argument jeżeli chcemy do inengo kontrolera, tez w cudzysłowie
                return RedirectToAction("index");
            }
            return View(obj);
        }
        
        //GET ONE EDIT CATEGORY
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            // różne sposoby na wyciąganie z bazy encji
            var category = _db.Categories.Find(id);
            // Find bazuje na primary key
            // var category = _db.Categories.SingleOrDefault(u => u.Id == id);
            // SingleOrDefault wyrzuci błąd jeżeli będzie wiecej niz jedno id, nie będzie w tym wypadku ale obsługa wyjątku nie zaszkodzi
            // jest jeszcze Single, który wyrzuca exception kiedy nie znajdzie niczego, powyższy tak nie zrobi
            // var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            // FirstOrDefault nie wywali exception jeżeli będzie więcej niż jeden wynik
            // var category = _db.Categories.SingleOrDefault(u => u.Id == id);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        
        // EDIT ONE CATEGORY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                // key, value - key musi być unikalny
                ModelState.AddModelError("DispErrMatchError", "Display Order cannot match the Name of category");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                // można dodać drugi argument jeżeli chcemy do inengo kontrolera, tez w cudzysłowie
                return RedirectToAction("index");
            }
            return View(obj);
        }

        //GET ONE DELETE CATEGORY
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // DELETE ONE CATEGORY
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                // można dodać drugi argument jeżeli chcemy do inengo kontrolera, tez w cudzysłowie
                return RedirectToAction("index");
            }
            return View(obj);
        }

    }
}
