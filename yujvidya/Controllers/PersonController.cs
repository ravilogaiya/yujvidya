using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Net;
using yujvidya.Managers;

namespace yujvidya
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonContext context;
        public PersonController(PersonContext context)
        {
            this.context = context;
        }

        public IEnumerable<Person> Get()
        {
            return this.context.Persons;
        }

        // students/?column=todate&order=0&filter=ravi&skip=1&take=1
        [HttpGet("students")]
        public IEnumerable<Student> GetStudents(string column, int order, string filter, int skip, int take, bool? inactive = null, DateTime? inactiveFrom = null)
        {
            var sortOrder = order == 1 ? " DESC" : "";
            var latestEnrollments = this.context.Enrollments.GroupBy(x => x.PersonId).Select(x => x.OrderByDescending(y => y.ToDate).First());
            var studentsQuery = this.context.Persons
                                    .Join(latestEnrollments, x => x.Id, y => y.PersonId, (x, y) => new
                                    {
                                        x.Id,
                                        x.FirstName,
                                        x.LastName,
                                        y.ToDate,
                                        x.Inactive,
                                        EnrollmentId = y.Id,
                                        x.MobileNumber
                                    });

            if (inactive.HasValue)
            {
                studentsQuery = studentsQuery.Where(x => x.Inactive == inactive.Value);

                if (inactiveFrom.HasValue)
                    studentsQuery = studentsQuery.Where(x => x.ToDate <= inactiveFrom);
            }

            if (!string.IsNullOrEmpty(filter))
                studentsQuery = studentsQuery.Where($"FirstName.ToLower().Contains(\"{filter.ToLower()}\") OR LastName.ToLower().Contains(\"{filter.ToLower()}\")");

            if (!string.IsNullOrEmpty(column))
                studentsQuery = studentsQuery.OrderBy($"{column}{sortOrder}");

            if (skip > 0)
                studentsQuery = studentsQuery.Skip(skip);

            if (take > 0)
                studentsQuery = studentsQuery.Take(take);

            var students = studentsQuery.Select(x => new Student
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                EnrollmentId = x.EnrollmentId,
                EnrolledUpto = x.ToDate,
                MobileNumber = x.MobileNumber
            });

            return students;
        }

        [HttpGet("students/{id}")]
        public StudentDetail GetStudentDetail(int id)
        {
            var person = this.Get(id);

            if (person == null)
                return null;

            var detail = this.context.PersonDetails.Single(x => x.PersonId == id);
            var careTakers = this.context.PersonCareTakes.Where(x => x.PersonId == id).OrderBy(x => x.Id).ToList();
            var enrollments = this.context.Enrollments.Where(x => x.PersonId == id).OrderBy(x => x.Id).ToList();

            var entrollmentTypes = this.context.EnrollmentTypes.ToList();
            var batchSchedules = this.context.BatchSchedules.ToList();

            return new StudentDetail() { Person = person, Details = detail, CareTakers = careTakers, Enrollments = enrollments };
        }

        [HttpGet("students/enrollment/{id}")]
        public Enrollment GetEnrollment(int id)
        {
            var enrollment = this.context.Enrollments.FirstOrDefault(x => x.Id == id);

            if (enrollment == null)
                return null;

            var entrollmentTypes = this.context.EnrollmentTypes.ToList();
            var batchSchedules = this.context.BatchSchedules.ToList();

            return enrollment;
        }

        [HttpGet("students/enrollment/latest/{personId}")]
        public Enrollment GetLatestEnrollment(int personId)
        {
            var enrollment = this.context.Enrollments.Where(x => x.PersonId == personId).OrderByDescending(x => x.ToDate).First();

            if (enrollment == null)
                return null;

            var entrollmentTypes = this.context.EnrollmentTypes.ToList();
            var batchSchedules = this.context.BatchSchedules.ToList();

            return enrollment;
        }

        [HttpPut("students/update")]
        public async Task<IActionResult> UpdateStudentDetail([FromBody] StudentDetail studentDetail)
        {
            var person = studentDetail.Person;
            var dbPerson = this.Get(person.Id);

            if (dbPerson == null)
                return StatusCode(400, $"A Person with id = {person.Id} not found");

            if (this.context.Persons.Any(x => x.Id != person.Id && x.FirstName == person.FirstName && x.LastName == person.LastName && x.MobileNumber == person.MobileNumber))
                return StatusCode(409, new { Message = "A person with same name and mobile number already exist" });

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Gender = person.Gender;
            dbPerson.BirthDate = person.BirthDate;
            dbPerson.MobileNumber = person.MobileNumber;

            var careTakers = studentDetail.CareTakers;
            var careTaker1 = careTakers.Count > 0 ? careTakers[0] : default(PersonCareTaker);
            var careTaker2 = careTakers.Count > 1 ? careTakers[1] : default(PersonCareTaker);

            this.VerifyAndAddMobileNumber(person, careTaker1, studentDetail.CareTakers.Last());

            var dbPersonDetail = this.context.PersonDetails.FirstOrDefault(x => x.PersonId == person.Id);

            if (dbPersonDetail.PersonId != person.Id && person.Id == studentDetail.Details.PersonId)
                return StatusCode(409, $"Details.PersonId mismatch with database");

            if (dbPersonDetail.Comments != studentDetail.Details.Comments)
            {
                dbPersonDetail.Comments = studentDetail.Details.Comments;
                dbPersonDetail.Date = DateTime.Now;
            }

            var dbCareTaker1 = UpdateCareTaker(person, careTaker1, out int code, out string message);

            if (code != 200)
                return StatusCode(code, message);

            var dbCareTaker2 = UpdateCareTaker(person, careTaker2, out code, out message);

            if (code != 200)
                return StatusCode(code, message);

            await this.context.SaveChangesAsync();

            return StatusCode(200, new
            {
                Person = dbPerson,
                Detail = dbPersonDetail,
                CareTaker1 = dbCareTaker1,
                CareTaker2 = dbCareTaker2
            });

        }

        private PersonCareTaker UpdateCareTaker(Person person, PersonCareTaker careTaker, out int code, out string message)
        {
            var dbCareTaker = this.context.PersonCareTakes.FirstOrDefault(x => x.Id == careTaker.Id);

            if (dbCareTaker.PersonId != person.Id && person.Id == careTaker.PersonId)
            {
                code = 409;
                message = $"CareTaker.PersonId mismatch with database";
            }

            dbCareTaker.FirstName = careTaker.FirstName;
            dbCareTaker.LastName = careTaker.LastName;
            dbCareTaker.MobileNumber = careTaker.MobileNumber;
            dbCareTaker.Type = careTaker.Type;

            code = 200;
            message = string.Empty;
            return dbCareTaker;
        }

        [HttpPost("students/enrollment/renew")]
        public async Task<IActionResult> RenewStudentEnrollment([FromBody] Enrollment enrollment)
        {
            var person = this.Get(enrollment.PersonId);

            if (person == null)
                return StatusCode(400, $"A Person with id = {enrollment.PersonId} not found");

            var activeEnrollment = this.context.Enrollments.FirstOrDefault(x => x.PersonId == enrollment.PersonId && x.ToDate > enrollment.FromDate);

            if (activeEnrollment != null)
                return StatusCode(409, $"Found active enrollment, enrollment from date cannot be before {activeEnrollment.ToDate}");

            enrollment.PaymentDate = DateTime.Now;

            this.context.Add(enrollment);

            var smsDetail = await NotificationMessageManager.SendSms(person.MobileNumber,
                                                                     MessageTemplate.RenewalMessageTemplate,
                                                                     person.FirstName, enrollment.ToDate);

            smsDetail.PersonId = person.Id;
            this.context.Add(smsDetail);

            await this.context.SaveChangesAsync();

            return StatusCode(201, new
            {
                Person = person,
                Enrollment = enrollment
            });
        }

        [HttpPut("students/enrollment/update")]
        public async Task<IActionResult> UpdateStudentEnrollment([FromBody] Enrollment enrollment)
        {
            var person = this.Get(enrollment.PersonId);

            if (person == null)
                return StatusCode(400, $"A Person with id = {enrollment.PersonId} not found");

            if (enrollment.FromDate > enrollment.ToDate)
                return StatusCode(409, $"Enrollment to date cannot be lesser than from date");

            var dbEnrollment = this.context.Enrollments.FirstOrDefault(x => x.Id == enrollment.Id);

            if (dbEnrollment == null)
                return StatusCode(400, $"Enrollment with id = {enrollment.Id} not found");

            if (dbEnrollment.PersonId != enrollment.PersonId)
                return StatusCode(409, $"Enrollment PersonId mismatch with database");

            var activeEnrollment = this.context.Enrollments.FirstOrDefault(x => x.PersonId == enrollment.PersonId &&
                                                                                x.ToDate < enrollment.FromDate &&
                                                                                x.Id != enrollment.Id);

            if (activeEnrollment != null)
                return StatusCode(409, $"Found active enrollment, enrollment from date cannot be before {activeEnrollment.ToDate}");

            dbEnrollment.Amount = enrollment.Amount;
            dbEnrollment.PreferredBatchScheduleId = enrollment.PreferredBatchScheduleId;
            dbEnrollment.EnrollmentTypeId = enrollment.EnrollmentTypeId;
            dbEnrollment.FromDate = enrollment.FromDate;
            dbEnrollment.ToDate = enrollment.ToDate;

            var smsDetail = await NotificationMessageManager.SendSms(person.MobileNumber,
                                                                     MessageTemplate.RegistrationMessageTemplate,
                                                                     person.FirstName, dbEnrollment.ToDate);

            smsDetail.PersonId = person.Id;
            this.context.Add(smsDetail);

            await this.context.SaveChangesAsync();

            return StatusCode(201, new
            {
                Person = person,
                Enrollment = enrollment
            });
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return this.context.Persons.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationData value)
        {
            if (value == null)
                return StatusCode(400, value);

            if (this.context.Persons.Any(x => x.FirstName == value.Person.FirstName && x.LastName == value.Person.LastName && x.MobileNumber == value.Person.MobileNumber))
                return StatusCode(409, new { Message = "A person with same name and mobile number already exist" });

            var person = this.context.Add(value.Person);

            this.VerifyAndAddMobileNumber(value.Person, value.CareTaker1, value.CareTaker2);

            this.context.SaveChanges();

            // Adding care takers
            if (value.CareTaker1 != null)
            {
                value.CareTaker1.PersonId = value.Person.Id;
                this.context.Add(value.CareTaker1);
            }

            if (value.CareTaker2 != null)
            {
                value.CareTaker2.PersonId = value.Person.Id;
                this.context.Add(value.CareTaker2);
            }

            if (value.Details != null)
            {
                value.Details.PersonId = value.Person.Id;
                value.Details.Date = DateTime.Now;
                this.context.Add(value.Details);
            }

            var smsDetail = await NotificationMessageManager.SendSms(value.Person.MobileNumber,
                                                                     MessageTemplate.RegistrationMessageTemplate,
                                                                     value.Person.FirstName, value.Enrollment.ToDate);

            smsDetail.PersonId = value.Person.Id;
            this.context.Add(smsDetail);

            value.Enrollment.PersonId = person.Entity.Id;
            value.Enrollment.AcknowledgementSent = smsDetail.Status > 0;
            value.Enrollment.PaymentDate = DateTime.Now;
            var enrollment = this.context.Add(value.Enrollment);

            this.context.SaveChanges();

            return StatusCode(201, new
            {
                RegistrationData = value,
                SmsDetail = smsDetail
            });
        }

        private void VerifyAndAddMobileNumber(Person person, PersonCareTaker careTaker1, PersonCareTaker careTaker2)
        {
            var dbMobilenumber = this.context.MobileNumbers.FirstOrDefault(x => x.Id == person.MobileNumber);

            GetMobileNumberName(person, careTaker1, careTaker2, out string firstName, out string lastName, out string personMobileNumber);

            if (personMobileNumber != person.MobileNumber)
            {
                dbMobilenumber = this.context.MobileNumbers.FirstOrDefault(x => x.Id == personMobileNumber);
            }

            if (dbMobilenumber == null)
            {
                var mobileNumber = new MobileNumber() { Id = personMobileNumber, FirstName = firstName, LastName = lastName };
                this.context.Add(mobileNumber);
            }
            else
            {
                dbMobilenumber.FirstName = firstName;
                dbMobilenumber.LastName = lastName;
            }
        }

        private static void GetMobileNumberName(Person person, PersonCareTaker careTaker1, PersonCareTaker careTaker2, out string firstName, out string lastName, out string personMobileNumber)
        {
            firstName = person.FirstName;
            lastName = person.LastName;
            personMobileNumber = person.MobileNumber;

            if (careTaker1 != null && careTaker1.MobileNumber == personMobileNumber)
            {
                firstName = careTaker1.FirstName;
                lastName = careTaker1.LastName;
            }
            else if (careTaker2 != null && careTaker2.MobileNumber == personMobileNumber)
            {
                firstName = careTaker2.FirstName;
                lastName = careTaker2.LastName;
            }
        }
    }
}