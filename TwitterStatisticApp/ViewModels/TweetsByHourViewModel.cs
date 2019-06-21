using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.ViewModels
{
    public class TweetsByHourViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int Count { get; set; }
    }
}
