using Application.DataAccess;
using Application.DataAccess.Repository.iRepository;
using Application.Models;
using Application.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of products and renders then in a view
        /// </summary>
        /// <returns>The view containing the list of products.</returns>
        [HttpGet]
        [Route("/Product")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        
        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };
            

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefualt(u => u.Id == id);
                return View(productVM);
            }

            
        }

        /// <summary>
        /// Used to create and update the product
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The view containing the product details.</returns>
        //POST
        [HttpPost]
        [Route("/Product/Upsert")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj)
        {

            if (ModelState.IsValid)
            {
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
            
            

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var category = _db.Categories.Find(id);
            var product = _unitOfWork.Product.GetFirstOrDefualt(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = _unitOfWork.Category.GetAll();
            var covertypes = _unitOfWork.CoverType.GetAll();
            var productViewModel = new ProductVM
            {
                Product = product,
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name}),
                CoverTypeList = covertypes.Select(ct => new SelectListItem { Value = ct.Id.ToString(), Text = ct.Name})
            };
            return View(productViewModel);
        }

        /// <summary>
        /// Used to delete the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The view of remaining products.</returns>
        //POST
        [HttpPost]
        [Route("/Product/Delete/{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefualt(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);

        }


    }
}
