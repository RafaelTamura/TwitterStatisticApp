using System;

namespace TwitterStatisticApp.Domain.Entities
{
    public class TweetsByTag
    {
        public TweetsByTag(Guid id, int count, string hashtag, string location, string languageCode, string languageName)
        {
            Id = id;
            Count = count;
            Hashtag = hashtag;
            Location = location;
            LanguageCode = languageCode;
            LanguageName = languageName;
        }

        public Guid Id { get; private set; }
        public int Count { get; private set; }
        public string Hashtag { get; private set; }
        public string Location { get; private set; }
        public string LanguageCode { get; private set; }
        public string LanguageName { get; private set; }
    }
}
