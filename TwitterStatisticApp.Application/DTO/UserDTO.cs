using System;

namespace TwitterStatisticApp.Application.DTO
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string IdStr { get; set; }
        public string ScreenName { get; set; }
        public int FollowersCount { get; set; }
        public string Location { get; set; }
        public string LanguageCode { get; set; }
    }
}
