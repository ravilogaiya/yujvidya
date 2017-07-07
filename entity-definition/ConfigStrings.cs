using System;
namespace yujvidya
{
    public static class ConfigStrings
    {
        public static readonly string ApiBaseUrl = $"http://{ConfigVariables.YV_API_HOST}:{ConfigVariables.YV_API_PORT}";
        public static readonly string DbConnectionString = $"User ID={ConfigVariables.YV_DB_USERID};Password={ConfigVariables.YV_DB_PASSWORD};Server={ConfigVariables.YV_DB_SERVER};Port={ConfigVariables.YV_DB_PORT};Database={ConfigVariables.YV_DB_NAME}{ConfigVariables.YV_ENV};Integrated Security=true;Pooling=true;";
        public static readonly string SmsUrl = ConfigVariables.YV_SMS_URL;
        public static readonly string SmsUser = ConfigVariables.YV_SMS_USER;
        public static readonly string SmsPassword = ConfigVariables.YV_SMS_PASSWORD;
        public static readonly string SmsSender = ConfigVariables.YV_SMS_SENDER;
    }
}
