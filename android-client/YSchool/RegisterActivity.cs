using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace YSchool
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);
            Button btnRegister = FindViewById<Button>(Resource.Id.btnRegister2);

            btnRegister.Click += async (sender, e) =>
            {
                var person = new Person
                {
                   // name = 
             
                };
                var json = await Register(API.Register);
                btnRegister.Text = json[0].name;
                // ParseAndDisplay (json);
            };

        }


        public async Task<List<Person>> Register(string url)
        {

            var uri = new Uri(url);
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var persons = new List<Person>();
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                      persons  = JsonConvert.DeserializeObject<List<Person>>(content);
                    }
                    return persons;
                }
             
                catch (System.Net.Http.HttpRequestException e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return persons;
                }
             
  
            }

            }
    }
}

//        private async Task<string> FetchWeatherAsync(string url)
//        {
//            // Create an HTTP web request using the URL:
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
//            request.ContentType = "application/json";
//            request.Method = "GET";

//            // Send the request to the server and wait for the response:
//            //using (WebResponse response = await request.GetResponseAsync())
//            //{
//            //    try
//            //    {
//            //        // Get a stream representation of the HTTP web response:
//            //        using (var stream = response.GetResponseStream())
//            //        {
//            //            // Use this stream to build a JSON document object:
//            //            //JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
//            //            var reader = new System.IO.StreamReader(stream, Encoding.UTF8);
//            //            string json = await Task.Run(() => reader.ReadToEnd());
//            //            return json;

//            //            // Return the JSON document:
//            //            // return jsonDoc;
//            //        }
//            //        }
//            //    catch (WebException ex)
//            //    {
//            //        WebResponse errorResponse = ex.Response;
//            //        using (System.IO.Stream responseStream = errorResponse.GetResponseStream())
//            //        {
//            //            System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
//            //            String errorText = reader.ReadToEnd();
//            //            // log errorText
//            //        }
//            //        throw;
//            //    }
//            //}
//        }
//    }
//}

//https://stackoverflow.com/questions/8270464/best-way-to-call-a-json-webservice-from-a-net-console
//string GET(string url)
//{
//    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
//    try
//    {
//        WebResponse response = request.GetResponse();
//        using (Stream responseStream = response.GetResponseStream())
//        {
//            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
//            return reader.ReadToEnd();
//        }
//    }
//    catch (WebException ex)
//    {
//        WebResponse errorResponse = ex.Response;
//        using (Stream responseStream = errorResponse.GetResponseStream())
//        {
//            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
//            String errorText = reader.ReadToEnd();
//            // log errorText
//        }
//        throw;
//    }
//}