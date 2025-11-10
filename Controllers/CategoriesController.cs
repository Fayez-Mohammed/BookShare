using Book_Sphere.Interfaces;
using Book_Sphere.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sphere.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICatecory CatRepo;
        private readonly IBook bookRepo;

        public CategoriesController(ICatecory catecory, IBook book)
        {
            this.CatRepo = catecory;
            this.bookRepo = book;
        }
        public IActionResult Index()
        {

            return View(CatRepo.GetCategories());
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult AddNew(Category catecory)
        {
            if (ModelState.IsValid)
            {
                CatRepo.Add(catecory);
                CatRepo.Safe();
                return RedirectToAction("Index");
            }
            return View("Add", catecory);
        }
        public IActionResult Delete(int id)
        {


            return View(CatRepo.GetCategoryById(id));
        }
        public IActionResult DeleteCurrent(Category catecory)
        {
            CatRepo.Remove(catecory);
            CatRepo.Safe();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {

            return View(CatRepo.GetCategoryById(id));
        }
        public IActionResult UpdateNew(Category catecory)
        {
            if (ModelState.IsValid)
            {
                CatRepo.Update(catecory); CatRepo.Safe();
                return RedirectToAction("Index");

            }
            return View("Edit", catecory.Id);
        }
        public IActionResult Details(int id)
        {
            ViewData["BookList"] = bookRepo.GetBooksByCategoryId(id).ToList();
            ViewData["CatName"] = CatRepo.GetCategoryById(id).Name;
            ViewData["CatName"] = CatRepo.GetCategoryById(id).Name;


            return View(bookRepo.GetBooksByCategoryId(id));
        }


    }
}
