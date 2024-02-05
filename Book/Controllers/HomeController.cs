using Application.DataAccess.Repository.iRepository;
using Application.Models;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics;



namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofWork.Product.GetAll();

            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _unitofWork.Product.GetFirstOrDefualt(u => u.Id == id)

            };
            return View(cartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
