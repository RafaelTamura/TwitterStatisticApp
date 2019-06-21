using System;
using System.Collections.Generic;
using TwitterStatisticApp.Domain.Entities.ObjectValues;

namespace TwitterStatisticApp.Domain.Entities
{
    public class Tweet
    {
        public Tweet(Guid id, string createdAt, string idStr, string userIdStr, IEnumerable<Hashtag> hashtags)
        {
            Id = id;
            CreatedAt = createdAt;
            IdStr = idStr;
            UserIdStr = userIdStr;
            Hashtags = hashtags;
        }

        public Guid Id { get; private set; }
        public string CreatedAt { get; private set; }
        public string IdStr { get; private set; }
        public string UserIdStr { get; private set; }
        public IEnumerable<Hashtag> Hashtags { get; private set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
