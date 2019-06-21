using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class TweetsByHourDTO
    {
        public Guid? Id { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int Count { get; set; }
    }
}
