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
using Hangfire.States;

namespace yujvidya
{
    [Route("api/[controller]")]
    public class EnrollmentTypeController : Controller
    {
        private readonly PersonContext context;
        public EnrollmentTypeController(PersonContext context)
        {
            this.context = context;
        }

        public IEnumerable<EnrollmentType> Get()
        {
            return this.context.EnrollmentTypes;
        }

        [HttpGet("{id}")]
        public EnrollmentType Get(int id)
        {
            return this.context.EnrollmentTypes.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]EnrollmentType value)
        {
            if (value == null)
                return StatusCode(400, value);

            value.FromDate = DateTime.Now;
            var enrollmentType = this.context.Add(value);

            await this.context.SaveChangesAsync();

            return StatusCode(201, new
            {
                EnrollmentType = enrollmentType.Entity
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]EnrollmentType value)
        {
            if (value == null)
                return StatusCode(400, value);

            var enrollmentType = this.context.EnrollmentTypes.FirstOrDefault(x => x.Id == value.Id);

            if (enrollmentType == null)
                return StatusCode(404, value.Id);

            enrollmentType.Amount = value.Amount;
            enrollmentType.Name = value.Name;

            await this.context.SaveChangesAsync();

            return StatusCode(200, new
            {
                EnrollmentType = enrollmentType
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollmentType = this.context.EnrollmentTypes.FirstOrDefault(x => x.Id == id);

            if (enrollmentType == null)
                return StatusCode(404, id);

            var statusMsg = string.Empty;
            if (this.context.Enrollments.Any(x => x.EnrollmentTypeId == id))
            {
                enrollmentType.Deleted = true;
                statusMsg = "Marked as deleted successful";
            }
            else
            {
                this.context.EnrollmentTypes.Remove(enrollmentType);
                statusMsg = "Delete successful";
            }

            await this.context.SaveChangesAsync();

            return StatusCode(200, statusMsg);
        }

        [HttpGet("name/{name}/{id}")]
        public bool IsEnrollmentNameExist(string name, int id)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var exist = this.context.EnrollmentTypes.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && x.Id != id);
            return exist;
        }
    }
}