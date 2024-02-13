using Application.DataAccess;
using Application.DataAccess.Repository.iRepository;
using Application.Models;
using Application.Models.ViewModel;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public CompanyController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves a list of companies and renders then in a view
        /// </summary>
        /// <returns>The view containing the list of companies.</returns>
        [HttpGet]
        [Route("/Company")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Index()
        {
            IEnumerable<Company> companyList = _unitOfWork.Company.GetAll();
            return View(companyList);
        }


        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefualt(u => u.Id == id);
                return View(company);
            }
             
            
        }

        //POST
        /// <summary>
        /// Used to create and update the company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>The view containing the company details.</returns>
        //POST
        [HttpPost]
        [Route("/Company/Upsert")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {

            if (ModelState.IsValid)
            {
                if(company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["update"] = "Company updated successfully";

                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(company);
            
            

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var company = _unitOfWork.Company.GetFirstOrDefualt(c => c.Id == id);
            if (company == null)
            {
                return NotFound();
            }
          
            return View(company);
        }

        //POST
        /// <summary>
        /// Used to delete the company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The view of remaining companies.</returns>
        //POST
        [HttpPost]
        [Route("/Company/Delete/{id}")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var company = _unitOfWork.Company.GetFirstOrDefualt(u => u.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Remove(company);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(company);

        }


    }
}
