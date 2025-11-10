using Book_Sphere.Interfaces;
using Book_Sphere.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Sphere.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly ICatecory catRepo;
        private readonly IBook bookRepo;

        public HomeController(ILogger<HomeController> logger, ICatecory catecory, IBook book)
        {
            _logger = logger;
            this.catRepo = catecory;
            this.bookRepo = book;
        }

        public IActionResult Index()
        {
            ViewData["HomeFlag"] = (bool)true;
            var Dic = new Dictionary<string, List<Book>>()
            {
                { "Most Popular",bookRepo.GetBooks().Where(s => s.Stars == 5).ToList()},
                {"Personal Productivity",  bookRepo.GetBooksByCategoryId(1).ToList()},
                {"Remote Working",   bookRepo.GetBooksByCategoryId(2).ToList()}
            };
            ViewData["BookDic"] = Dic;


            ViewData["AllBooks"] = bookRepo.GetBooks().ToList();
            ViewData["Popular"] = bookRepo.GetBooks().Where(s => s.Stars == 5).ToList();
            ViewData["Productivity"] = bookRepo.GetBooksByCategoryId(1).ToList();
            ViewData["Remote"] = bookRepo.GetBooksByCategoryId(2).ToList();
            return View(catRepo.GetCategories().Where(c => c.Id < 5).ToList());
        }

        public IActionResult Privacy()
        {
            ViewData["HomeFlag"] = (bool)true;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
