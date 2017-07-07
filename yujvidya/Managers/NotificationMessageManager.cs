using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using yujvidya.Attributes;
using yujvidya.Models;

namespace yujvidya.Managers
{
    public class NotificationMessageManager
    {
        public static async Task<SmsDetail> SendSms(string mobileNumber, MessageTemplate messageTemplate, params object[] paramters)
        {
            var smsChannelAttribute = messageTemplate.GetAttributeOfType<SmsChannelAttribute>();
            var message = string.Format(smsChannelAttribute.MessageTemplate, paramters);

            return await SendSmsViaSmsGatewayHub(mobileNumber, message, smsChannelAttribute.Type == SmsChannelType.Transactional ? 2 : 1);
        }

        private static async Task<SmsDetail> SendSmsViaSmsGatewayHub(string mobileNumber, string message, int channel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigStrings.SmsUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var newUser = new
                {
                    Account = new
                    {
                        User = ConfigStrings.SmsUser,
                        Password = ConfigStrings.SmsPassword,
                        SenderId = ConfigStrings.SmsSender,
                        Channel = channel,
                    },
                    Messages = new[] { new { Number = mobileNumber, Text = message } }
                };
                var stringContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
                HttpResponseMessage reponse = await client.PostAsync("/api/mt/SendSMS", stringContent);

                var smsDetail = new SmsDetail { MobileNumber = mobileNumber, Message = message };

                if (reponse.IsSuccessStatusCode)
                {
                    var value = await reponse.Content.ReadAsStringAsync();
                    var def = new { ErrorCode = "", ErrorMessage = "" };
                    dynamic result = JsonConvert.DeserializeAnonymousType(value, def);
                    smsDetail.Status = result.ErrorCode == "000" ? SmsStatus.Sent : SmsStatus.SentFailed;
                    smsDetail.StatusDescription = result.ErrorMessage;
                }
                else
                {
                    smsDetail.Status = SmsStatus.ServerError;
                    smsDetail.StatusDescription = "Error occured while calling message server";
                }

                return smsDetail;
            }
        }
    }
}
