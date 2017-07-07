using System;
namespace yujvidya
{
    public static class ConfigVariables
    {
        public static readonly string YV_ENV = "-dev";
        public static readonly string YV_API_HOST = Environment.GetEnvironmentVariable("YV_API_HOST");
        public static readonly string YV_API_PORT = Environment.GetEnvironmentVariable("YV_API_PORT");

        public static readonly string YV_DB_USERID = Environment.GetEnvironmentVariable("YV_DB_USERID");
        public static readonly string YV_DB_PASSWORD = Environment.GetEnvironmentVariable("YV_DB_PASSWORD");
        public static readonly string YV_DB_SERVER = Environment.GetEnvironmentVariable("YV_DB_SERVER");
        public static readonly string YV_DB_PORT = Environment.GetEnvironmentVariable("YV_DB_PORT");
        public static readonly string YV_DB_NAME = Environment.GetEnvironmentVariable("YV_DB_NAME");

        public static readonly string YV_SMS_URL = Environment.GetEnvironmentVariable("YV_SMS_URL");
        public static readonly string YV_SMS_USER = Environment.GetEnvironmentVariable("YV_SMS_URL");
        public static readonly string YV_SMS_PASSWORD = Environment.GetEnvironmentVariable("YV_SMS_URL");
        public static readonly string YV_SMS_SENDER = Environment.GetEnvironmentVariable("YV_SMS_URL");

        static ConfigVariables()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Production")
                YV_ENV = "";

            else if (env == "Staging")
                YV_ENV = "-stg";
        }
    }
}
