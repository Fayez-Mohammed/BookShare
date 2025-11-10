using Book_Sphere.Interfaces;
using Book_Sphere.Models;
using Book_Sphere.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sphere.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBook bookRepo;
        private readonly ICatecory catRepo;

        public BooksController(IBook book, ICatecory catecory)
        {
            this.bookRepo = book;
            this.catRepo = catecory;
        }
        public IActionResult Index()
        {


            return View(bookRepo.GetBooks());
        }
        public IActionResult Add()
        {
            ViewData["DeptList"] = catRepo.GetCategories().ToList();
            return View();
        }
        public IActionResult AddNew(Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepo.Add(book);
                bookRepo.Safe();
                return RedirectToAction("Index");
            }
            ViewData["DeptList"] = catRepo.GetCategories().ToList();
            return View("Add", book);
        }
        public IActionResult Delete(int id)
        {


            return View(bookRepo.GetBookById(id));
        }
        public IActionResult DeleteCurrent(Book book)
        {
            bookRepo.Remove(book);
            bookRepo.Safe();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewData["DeptList"] = catRepo.GetCategories().ToList();

            return View(bookRepo.GetBookById(id));
        }
        public IActionResult UpdateNew(Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepo.Update(book); bookRepo.Safe();
                return RedirectToAction("Index");

            }
            ViewData["DeptList"] = catRepo.GetCategories().ToList();
            return View("Edit", book.Id);
        }
        public IActionResult Details(int id)
        {
            Book book = bookRepo.GetBookById(id);
            ViewData["CName"] = catRepo.GetCategoryById(book.CategoryId).Name;
            return View(bookRepo.GetBookById(id));
        }
        public IActionResult AfterSearch(SearchViewModel searchVM)
        {
            if (string.IsNullOrWhiteSpace(searchVM.SearchResult))
                return View("Search", new List<Book>());


            return View("Search", bookRepo.SearchByTitle(searchVM.SearchResult));
        }
        public IActionResult Read(int id)
        {
            Book book = bookRepo.GetBookById(id);
            return View(book);
        }
        //public IActionResult Search()
        //{
        //    return View();
        //}
        public IActionResult Favor()
        {
            return View(bookRepo.FavorList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Favorite(int id)
        {
            var book = bookRepo.GetBookById(id);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            book.Favor = !book.Favor; // toggle
            bookRepo.Update(book);
            bookRepo.Safe();

            return Json(new { success = true, favor = book.Favor });
        }




    }
}
