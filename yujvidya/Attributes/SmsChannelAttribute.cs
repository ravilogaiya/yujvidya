using System;
namespace yujvidya.Attributes
{
    public class SmsChannelAttribute : Attribute
    {
        public SmsChannelAttribute(SmsChannelType type, string messageTemplate)
        {
            this.Type = type;
            this.MessageTemplate = messageTemplate;
        }

        public SmsChannelType Type
        {
            get;
            set;
        }

        public string MessageTemplate
        {
            get;
            set;
        }
    }
}
