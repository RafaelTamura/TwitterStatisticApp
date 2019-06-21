using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStatisticApp.Application.DTO
{
    public class StatusesDTO
    {
        public IEnumerable<TweetDTO> Tweet { get; set; }
    }
}
