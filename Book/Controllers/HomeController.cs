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


        /// <summary>
        /// Retrieves a list of products and renders then in a view
        /// </summary>
        /// <returns>The view containing the list of products.</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofWork.Product.GetAll();

            if(productList is null)
            {
                return NotFound();
            }

            return View(productList);
        }

        /// <summary>
        /// Retrieves the product details to add into shopping cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The view of details products.</returns>
        [HttpGet]
        [Route("/Home/Details/{id}")]
        [ProducesResponseType(typeof(ShoppingCart),200)]
        [ProducesResponseType(typeof(string),404)]
        [ProducesResponseType(typeof(string),500)]
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
