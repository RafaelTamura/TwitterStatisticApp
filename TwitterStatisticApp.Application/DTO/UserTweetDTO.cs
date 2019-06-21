using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class UserTweetDTO
    {
        public string id_str { get; set; }
        public string screen_name { get; set; }
        public int followers_count { get; set; }
        public string location { get; set; }
        public string lang { get; set; }
    }
}
