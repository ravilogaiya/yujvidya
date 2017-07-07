using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace yujvidya
{
    [Route("api/[controller]")]
    public class BatchScheduleController : Controller
    {
        private readonly PersonContext context;
        public BatchScheduleController(PersonContext context)
        {
            this.context = context;
        }

        public IEnumerable<BatchSchedule> Get()
        {
            return this.context.BatchSchedules;
        }

        [HttpGet("{id}")]
        public BatchSchedule Get(int id)
        {
            return this.context.BatchSchedules.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]BatchSchedule value)
        {
            if (value == null)
                return StatusCode(400, value);

            var batchSchedule = this.context.Add(value);

            await this.context.SaveChangesAsync();

            return StatusCode(201, new
            {
                BatchSchedule = batchSchedule.Entity
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]BatchSchedule value)
        {
            if (value == null)
                return StatusCode(400, value);

            var batchSchedule = this.context.BatchSchedules.FirstOrDefault(x => x.Id == value.Id);

            if (batchSchedule == null)
                return StatusCode(404, value);

            batchSchedule.Days = value.Days;
            batchSchedule.EndTime = value.EndTime;
            batchSchedule.StartTime = value.StartTime;
            batchSchedule.Type = value.Type;

            await this.context.SaveChangesAsync();

            return StatusCode(201, new
            {
                BatchSchedule = batchSchedule
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return StatusCode(400, 0);

            var batchSchedule = this.context.BatchSchedules.FirstOrDefault(x => x.Id == id);

            if (batchSchedule == null)
                return StatusCode(404, 0);

            var statusMsg = string.Empty;
            if (this.context.Enrollments.Any(x => x.PreferredBatchScheduleId == id))
            {
                batchSchedule.Deleted = true;
                statusMsg = "Marked as deleted successful";
            }
            else
            {
                this.context.BatchSchedules.Remove(batchSchedule);
                statusMsg = "Delete successful";
            }

            await this.context.SaveChangesAsync();

            return StatusCode(201, statusMsg);
        }
    }
}