using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yujvidya.Models;
using yujvidya.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace yujvidya.Controllers
{
    public class StudentsController : Controller
    {
        private DataService dataService;

        public StudentsController()
        {
            this.dataService = new DataService();
        }

        // GET: Students
        [HttpGet]
        public async Task<IActionResult> Index(
            string sortColumn,
            string sortOrder,
            string currentFilter,
            string searchString)
        {
            int.TryParse(sortOrder, out int desc);
            var filter = string.IsNullOrEmpty(searchString) ? currentFilter : searchString;

            ViewData["CurrentSort"] = desc == 1 ? "0" : "1";
            ViewData["CurrentFilter"] = filter;

            var students = await this.dataService.GetStudents(sortColumn, desc, filter);

            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            await FillViewBagForEnrollment();

            return View(new RegistrationData() { Enrollment = new Enrollment() { FromDate = DateTime.Now } });
        }

        private async Task FillViewBagForEnrollment()
        {
            ViewBag.EnrollmentTypes = await this.dataService.GetEnrollmentTypes();

            var batchSchedules = await this.dataService.GetBatchSchedules();
            ViewBag.BatchSchedules = batchSchedules.Select(x => new { Id = x.Id, StartTime = x.StartTime.ToString("hh:mm tt") });
        }

        [HttpGet("students/detail/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            return await GetById(id);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return await GetById(id);
        }

        public async Task<IActionResult> EditEnrollment(int id)
        {
            await this.FillViewBagForEnrollment();
            return await GetEnrollmentById(id);
        }

        [HttpGet("students/renewenrollment/{personId}")]
        public async Task<IActionResult> RenewEnrollment(int personId)
        {
            await this.FillViewBagForEnrollment();
            var latestEnrollment = await this.dataService.GetLatestEnrollment(personId);

            var enrollment = new Enrollment() { PersonId = personId, FromDate = latestEnrollment.ToDate.AddDays(1), ToDate = latestEnrollment.ToDate.AddDays(1) };

            return base.View(enrollment);
        }

        private async Task<IActionResult> GetEnrollmentById(int enrollmentId)
        {
            if (enrollmentId == 0)
            {
                return NotFound();
            }

            var enrollment = await dataService.GetEnrollment(enrollmentId);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        private async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var enrollmentType = await dataService.GetStudentDetail(id);

            if (enrollmentType == null)
            {
                return NotFound();
            }

            return View(enrollmentType);
        }

        [HttpPost, ActionName("renewenrollment")]
        public async Task<IActionResult> RenewEnrollment(Enrollment enrollment)
        {
            // Do not know id value is bidined to person id from View. Setting to zero to create new record
            enrollment.Id = 0;
            var addedEnrollment = await dataService.AddEnrollment(enrollment);
            return this.RedirectBasedOnResult(addedEnrollment);
        }

        [HttpPost, ActionName("saveenrollment")]
        public async Task<IActionResult> SaveEnrollment(Enrollment enrollment)
        {
            var updateResult = await dataService.UpadateEnrollment(enrollment);
            return this.RedirectBasedOnResult(updateResult);
        }

        [HttpPost, ActionName("edit")]
        public async Task<IActionResult> EditPost(StudentDetail strudentDetail)
        {
            var updateResult = await dataService.UpdateStrudent(strudentDetail);
            return this.RedirectBasedOnResult(updateResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationData registationData)
        {
            var addedEnrollmentType = await dataService.Register(registationData);
            return this.RedirectBasedOnResult(addedEnrollmentType);
        }

        [HttpGet]
        public async Task<JsonResult> GetDefaultAmountAndDueDate(int id, DateTime fromDate)
        {
            var enrollmentType = await this.dataService.GetEnrollmentType(id);

            if (enrollmentType == null)
                return Json(new { amount = 0, toDate = fromDate });

            DateTime toDateTime;
            switch (enrollmentType.DurationType)
            {
                case DurationType.Days:
                    toDateTime = fromDate.AddDays(enrollmentType.Duration);
                    break;

                case DurationType.Months:
                    toDateTime = fromDate.AddMonths(enrollmentType.Duration);
                    break;

                case DurationType.Years:
                    toDateTime = fromDate.AddYears(enrollmentType.Duration);
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }

            var toDate = toDateTime.ToString("yyyy-MM-dd");
            return Json(new { enrollmentType.Amount, toDate });
        }
    }
}
