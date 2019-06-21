using System;

namespace TwitterStatisticApp.Domain.Entities
{
    public class User
    {
        public User(Guid id, string idStr, string screenName, int followersCount, string location, string languageCode)
        {
            Id = id;
            IdStr = idStr;
            ScreenName = screenName;
            FollowersCount = followersCount;
            Location = location;
            LanguageCode = languageCode;
        }

        public Guid Id { get; private set; }
        public string IdStr { get; private set; }
        public string ScreenName { get; private set; }
        public int FollowersCount { get; private set; }
        public string Location { get; private set; }
        public string LanguageCode { get; private set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
