using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yujvidya.Models;
using yujvidya.Services;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace yujvidya.Controllers
{
    public class BatchScheduleController : Controller
    {
        DataService dataService;

        public BatchScheduleController()
        {
            dataService = new DataService();
        }

        public async Task<IActionResult> Index()
        {
            var batchSchedules = await dataService.GetBatchSchedules();
            return View(batchSchedules.OrderBy(x => new DateTime(2017, 1, 1, x.StartTime.Hour, x.StartTime.Minute, 0)));
        }

        // GET: Courses/Delete/5
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
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,Type,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")] BatchSchedule batchSchedule)
        {
            var addedbatchSchedule = await dataService.AddBatchSchedule(batchSchedule);
            var result = addedbatchSchedule as ObjectResult;

            if (result.StatusCode.IsSuccessStatusCode())
                return RedirectToAction("Index");

            return BadRequest(result.Value);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost([Bind("Id,StartTime,EndTime,Type,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")] BatchSchedule batchSchedule)
        {
            var updateResult = await dataService.UpdateBatchSchedule(batchSchedule);
            var result = updateResult as ObjectResult;

            if (result.StatusCode.IsSuccessStatusCode())
                return RedirectToAction("Index");

            return BadRequest(result.Value);
        }


        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteResult = await dataService.DeleteBatchSchedule(id);
            var result = deleteResult as ObjectResult;

            if (result.StatusCode.IsSuccessStatusCode())
                return RedirectToAction("Index");

            return BadRequest(result.Value);
        }

        private async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound($"Id = {id}, not found");
            }

            var enrollmentType = await dataService.GetBatchSchedule(id.Value);

            if (enrollmentType == null)
            {
                return NotFound($"Id = {id}, not found");
            }

            return View(enrollmentType);
        }
    }
}