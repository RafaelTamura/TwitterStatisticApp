using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class TweetsByTagDTO
    {
        public Guid? Id { get; set; }
        public int Count { get; set; }
        public string Hashtag { get; set; }
        public string Location { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
    }
}
