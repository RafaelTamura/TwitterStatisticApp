using System;

namespace TwitterStatisticApp.Domain.Entities
{
    public class TweetsByHour
    {
        public TweetsByHour(Guid id, DateTime date, int hour, int count)
        {
            Id = id;
            Date = date;
            Hour = hour;
            Count = count;
        }

        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public int Hour { get; private set; }
        public int Count { get; private set; }
    }
}
