using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class TwitterResponseDTO
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string followers_count { get; set; }
    }
}
