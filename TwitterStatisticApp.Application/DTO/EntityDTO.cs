using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class EntityDTO
    {
        public IEnumerable<HashtagDTO> Hashtags { get; set; }
    }
}
