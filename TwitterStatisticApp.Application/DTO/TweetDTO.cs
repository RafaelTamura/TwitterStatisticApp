using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class TweetDTO
    {
        public Guid? Id { get; set; }
        public string CreatedAt { get; set; }
        public string IdStr { get; set; }
        public IList<HashtagDTO> Hashtags { get; set; }
        public UserDTO User { get; set; }
    }
}
