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
                //create
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            //var category = _db.Categories.Find(id);
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefualt(u => u.Id == id);
                return View(productVM);
                //update
            }

            
        }

        //POST
        [HttpPost]
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

        //POST
        [HttpPost]
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
