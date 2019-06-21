using System;

namespace TwitterStatisticApp.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string IdStr { get; set; }
        public string ScreenName { get; set; }
        public int FollowersCount { get; set; }
    }
}
