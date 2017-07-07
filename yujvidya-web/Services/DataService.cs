using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using yujvidya.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace yujvidya.Services
{
    public class DataService
    {

        private readonly string baseServerUrl = ConfigStrings.ApiBaseUrl;

        public async Task<IEnumerable<Student>> GetStudents(string column, int order, string filter)
        {
            return await GetCollection<Student>("person/students", new { column, order, filter });
        }

        public async Task<IEnumerable<BatchSchedule>> GetBatchSchedules()
        {
            return await GetCollection<BatchSchedule>("batchschedule");
        }

        public async Task<BatchSchedule> GetBatchSchedule(int id)
        {
            return await GetById<BatchSchedule>("batchschedule", id);
        }

        public async Task<IActionResult> DeleteBatchSchedule(int id)
        {
            return await Delete("batchschedule", id);
        }

        public async Task<IActionResult> UpdateBatchSchedule(BatchSchedule batchSchedule)
        {
            return await Update("batchschedule", batchSchedule);
        }

        public async Task<IActionResult> AddBatchSchedule(BatchSchedule batchSchedule)
        {
            return await Add("batchschedule", batchSchedule);
        }

        internal async Task<IActionResult> Register(RegistrationData registationData)
        {
            return await Add("person", registationData);
        }

        public async Task<IEnumerable<EnrollmentType>> GetEnrollmentTypes()
        {
            return await GetCollection<EnrollmentType>("enrollmenttype");
        }

        internal async Task<StudentDetail> GetStudentDetail(int id)
        {
            var studentDetail = await GetById<StudentDetail>("person/students", id);
            studentDetail.Enrollments.OrderByDescending(x => x.ToDate).First().AllowEdit = true;

            return studentDetail;
        }

        public async Task<EnrollmentType> GetEnrollmentType(int id)
        {
            return await GetById<EnrollmentType>("enrollmenttype", id);
        }

        internal async Task<Enrollment> GetEnrollment(int id)
        {
            return await GetById<Enrollment>("person/students/enrollment", id);
        }

        internal async Task<Enrollment> GetLatestEnrollment(int personId)
        {
            return await GetById<Enrollment>("person/students/enrollment/latest", personId);
        }

        public bool IsEnrollmentTypeNameUnique(string name, int id)
        {
            return IsUnique("enrollmenttype/name", name, id).Result;
        }

        public async Task<IActionResult> UpdateStrudent(StudentDetail strudentDetail)
        {
            return await Update("person/students/update", strudentDetail);
        }

        public async Task<IActionResult> UpadateEnrollment(Enrollment enrollment)
        {
            return await Update("person/students/enrollment/update", enrollment);
        }

        public async Task<IActionResult> DeleteEnrollmentType(int id)
        {
            return await Delete("enrollmenttype", id);
        }

        public async Task<IActionResult> UpdateEnrollmentType(EnrollmentType type)
        {
            return await Update("enrollmenttype", type);
        }

        public async Task<IActionResult> AddEnrollmentType(EnrollmentType type)
        {
            return await Add("enrollmenttype", type);
        }

        public async Task<IActionResult> AddEnrollment(Enrollment enrollment)
        {
            return await Add("person/students/enrollment/renew", enrollment);
        }

        private async Task<bool> IsUnique<T>(string uriPart, T valueToCheck, int id)
        {
            using (var client = new HttpClient())
            {
                IntializeClient(client);

                var reponse = await client.GetAsync($"/api/{uriPart}/{valueToCheck}/{id}");

                if (reponse.IsSuccessStatusCode)
                {
                    var jsonString = await reponse.Content.ReadAsStringAsync();
                    var batchSchedule = JsonConvert.DeserializeObject<bool>(jsonString);
                    return !batchSchedule;
                }
            }

            return false;
        }

        private async Task<IActionResult> Add<T>(string uriPart, T value)
        {
            using (var client = new HttpClient())
            {
                IntializeClient(client);

                using (var json = ConvertToJsonString(value))
                {
                    var response = await client.PostAsync($"/api/{uriPart}", json);
                    return await response.ToActionResult();
                }
            }
        }

        private async Task<IActionResult> Update<T>(string uriPart, T value)
        {
            using (var client = new HttpClient())
            {
                IntializeClient(client);

                using (var json = ConvertToJsonString(value))
                {
                    var response = await client.PutAsync($"/api/{uriPart}", json);
                    return await response.ToActionResult();
                }
            }
        }

        private async Task<IActionResult> Delete(string uriPart, int id)
        {
            using (var client = new HttpClient())
            {
                IntializeClient(client);

                var response = await client.DeleteAsync($"/api/{uriPart}/{id}");
                return await response.ToActionResult();
            }
        }

        private async Task<T> GetById<T>(string uriPart, int id)
        {
            using (var client = new HttpClient())
            {
                IntializeClient(client);

                var reponse = await client.GetAsync($"/api/{uriPart}/{id}");

                if (reponse.IsSuccessStatusCode)
                {
                    var jsonString = await reponse.Content.ReadAsStringAsync();
                    var batchSchedule = JsonConvert.DeserializeObject<T>(jsonString);
                    return batchSchedule;
                }
            }

            return default(T);
        }

        private async Task<IEnumerable<T>> GetCollection<T>(string uriPart, dynamic parameters = null)
        {
            string url = GetUrlWithParameters(uriPart, parameters);

            using (var client = new HttpClient())
            {
                IntializeClient(client);

                var reponse = await client.GetAsync(url);

                if (reponse.IsSuccessStatusCode)
                {
                    var jsonString = await reponse.Content.ReadAsStringAsync();
                    var batchSchedules = JsonConvert.DeserializeObject<List<T>>(jsonString);
                    return batchSchedules;
                }
            }

            return Enumerable.Empty<T>();
        }

        private static string GetUrlWithParameters(string uriPart, dynamic parameters)
        {
            var url = $"/api/{uriPart}";
            if (parameters != null)
            {
                var pairs = new Dictionary<string, string>();
                Type type = parameters.GetType();
                foreach (var property in type.GetProperties())
                {
                    var value = property.GetValue(parameters);
                    if (value != null)
                    {
                        pairs.Add(property.Name, value.ToString());
                    }
                }

                url = QueryHelpers.AddQueryString(url, pairs);
            }

            return url;
        }

        private void IntializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(baseServerUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static StringContent ConvertToJsonString(object value)
        {
            return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        }
    }
}