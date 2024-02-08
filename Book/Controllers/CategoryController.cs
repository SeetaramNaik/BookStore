using Application.DataAccess;
using Application.DataAccess.Repository.iRepository;
using Application.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication.Controllers
{

    //[ApiController]
    //[Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        
        public CategoryController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET
        /// <summary>
        /// To get all the category list
        /// </summary>
        /// <returns> This endpoint returns a view of the Categories.</returns>
        [HttpGet]
        [Route("/Category")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        /// <summary>
        /// To create a category for the book 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> This endpoint returns a view of the Categories.</returns>
        [HttpPost]
        [Route("/Category/Create")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }


        //GET
        /// <summary>
        /// To fetch the category detail to be updated
        /// </summary>
        /// <param name="id"></param>
        /// <returns> This endpoint returns a view of the Categories.</returns>
        [HttpGet]
        [Route("/Category/Edit/{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //var category = _db.Categories.Find(id);
            var category = _unitOfWork.Category.GetFirstOrDefualt(c=>c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        /// <summary>
        /// To edit a category for the book 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> This endpoint returns a view of the updated Categories.</returns>
        [HttpPost]
        [Route("/Category/Edit/{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
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
            var category = _unitOfWork.Category.GetFirstOrDefualt(c=>c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        /// <summary>
        /// To delete a category of the book 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> This endpoint returns a view of the updated Categories.</returns>
        [HttpPost]
        [Route("/Category/Delete/{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }


    }
}
