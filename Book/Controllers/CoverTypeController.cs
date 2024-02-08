using Application.DataAccess;
using Application.DataAccess.Repository.iRepository;
using Application.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public CoverTypeController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET
        /// <summary>
        /// To get all the covertype list
        /// </summary>
        /// <returns> This endpoint returns a view of the Cover Types.</returns>
        [HttpGet]
        [Route("/CoverType")]
        [ProducesResponseType(typeof(CoverType), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        /// <summary>
        /// To create a new cover type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> This endpoint returns a view of the Cover Type list.</returns>
        [HttpPost]
        [Route("/CoverType/Create")]
        [ProducesResponseType(typeof(CoverType), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }


        //GET

        /// <summary>
        /// To get a cover type to be updated
        /// </summary>
        /// <param name="id"></param>
        /// <returns> This endpoint returns a view of the updated Cover Type list.</returns>
        [HttpGet]
        [Route("/CoverType/Edit/{id}")]
        [ProducesResponseType(typeof(CoverType), 200)]
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
            var covertype = _unitOfWork.CoverType.GetFirstOrDefualt(c=>c.Id == id);
            if (covertype == null)
            {
                return NotFound();
            }
            return View(covertype);
        }

        //POST
        /// <summary>
        /// To update the Cover Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> This endpoint returns a view of the updated Cover Type list.</returns>
        [HttpPost]
        [Route("/CoverType/Create/{id}")]
        [ProducesResponseType(typeof(CoverType), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
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
            var covertype = _unitOfWork.CoverType.GetFirstOrDefualt(c=>c.Id == id);
            if (covertype == null)
            {
                return NotFound();
            }
            return View(covertype);
        }

        //POST
        /// <summary>
        /// To delete the Cover Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> This endpoint returns a view of the updated Cover Type list.</returns>
        [HttpPost]
        [Route("/CoverType/Delete/{id}")]
        [ProducesResponseType(typeof(CoverType), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Remove(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }


    }
}
