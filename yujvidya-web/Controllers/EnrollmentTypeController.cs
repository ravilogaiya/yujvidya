using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using yujvidya.Models;
using yujvidya.Services;

namespace yujvidya.Controllers
{
    public class EnrollmentTypeController : Controller
    {
        DataService dataService;

        public EnrollmentTypeController()
        {
            this.dataService = new DataService();
        }

        public async Task<IActionResult> Index()
        {
            var enrollmentTypes = await dataService.GetEnrollmentTypes();
            return View(enrollmentTypes);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await GetById(id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await GetById(id);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Amount")] EnrollmentType enrollmentType)
        {
            var addedEnrollmentType = await dataService.AddEnrollmentType(enrollmentType);
            return this.RedirectBasedOnResult(addedEnrollmentType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await dataService.DeleteEnrollmentType(id);
            return this.RedirectBasedOnResult(deleted);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost([Bind("Id,Name,Amount")] EnrollmentType enrollmentType)
        {
            var updateResult = await dataService.UpdateEnrollmentType(enrollmentType);
            return this.RedirectBasedOnResult(updateResult);
        }

        [HttpGet]
        public JsonResult IsEnrollmentTypeNameExists(string name, int? id)
        {
            var result = dataService.IsEnrollmentTypeNameUnique(name, id.HasValue ? id.Value : 0);
            return Json(result);
        }

        private async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var enrollmentType = await dataService.GetEnrollmentType(id.Value);

            if (enrollmentType == null)
            {
                return NotFound();
            }

            return View(enrollmentType);
        }
    }
}