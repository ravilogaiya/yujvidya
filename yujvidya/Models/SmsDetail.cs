using System;
using System.ComponentModel.DataAnnotations;

namespace yujvidya.Models
{
    public class SmsDetail
    {
        [Key]
        public int Id
        {
            get;
            set;
        }

        public int PersonId
        {
            get;
            set;
        }

        public string MobileNumber
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public SmsStatus Status
        {
            get;
            set;
        }

        public string StatusDescription
        {
            get;
            set;
        }
    }
}
