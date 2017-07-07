using System;
using yujvidya.Attributes;

namespace yujvidya
{
    public enum DueDateNotificationLevel
    {
        First = -2, // - before 2 days
        Second = 0, // - On due date
        Third = 1, // - One day after due date
        Fourth = 7, // - After 7 days
        Fifth = 10 // - Make inactive after 10 days
    }

    public enum SmsStatus
    {
        DeliveredFailed = -2,
        SentFailed = -1,
        ServerError = 0,
        Sent = 1,
        Delivered = 2
    }

    public enum SmsChannelType
    {
        Transactional = 1,
        Promotional = 2
    }

    public enum MessageTemplate
    {
        [SmsChannel(SmsChannelType.Transactional, "Dear {0}, thank you for joining Yuj Vidya. Your are enrolled upto {1}")]
        RegistrationMessageTemplate = 1,

        [SmsChannel(SmsChannelType.Transactional, "Dear {0}, thank you for renewing. Your enrollment has been renewed upto {1}")]
        RenewalMessageTemplate = 2,

        [SmsChannel(SmsChannelType.Transactional, "Dear {0}, your enrollment has been updated to {1}")]
        EnrollmentUpdateMessageTemplate = 3,

        [SmsChannel(SmsChannelType.Promotional, "Dear {0}, your enrollment will be expired in {1} days. Please renew.")]
        PriorDueDateTemplate = 4,

        [SmsChannel(SmsChannelType.Promotional, "Dear {0}, your enrollment will expire tomorrow. Please renew.")]
        OneDayPriorDueDateTemplate = 5,

        [SmsChannel(SmsChannelType.Promotional, "Dear {0}, your enrollment is expiring today. Please renew.")]
        OnDueDateTemplate = 6,

        [SmsChannel(SmsChannelType.Promotional, "Dear {0}, your enrollment had expired yesterday. Please renew.")]
        OneDayOverDueDateTemplate = 7,

        [SmsChannel(SmsChannelType.Promotional, "Dear {0}, your enrollment had expired on {1}. Please renew.")]
        OverDueDateTemplate = 8,
    }
}
