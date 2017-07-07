using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace YSchool
{
    public static class API
    {
        private static string host = "http://192.168.56.1";
        private static string port = "44698";
        public static string Register = $"http://{host}:{port}/api/person";
        public static string PersonsAll = $"http://{host}:{port}/api/person";
        public static string BatchSchedule = $"http://{host}:{port}/api/batchschedule";
    }
}