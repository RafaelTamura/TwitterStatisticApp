using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.ViewModels
{
    public class TweetsByTagViewModel
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public string Hashtag { get; set; }
        public string Location { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
    }
}
