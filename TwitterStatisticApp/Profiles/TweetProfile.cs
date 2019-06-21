using AutoMapper;
using System;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.Domain.Entities.ObjectValues;

namespace TwitterStatisticApp.Profiles
{
    public class TweetProfile : Profile
    {
        public TweetProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<Tweet, TweetDTO>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<TweetDTO, Tweet>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id))
                                        .ForMember(d => d.UserIdStr, o => o.MapFrom(f => f.User.IdStr));
            CreateMap<TweetDTO, User>().ForMember(d => d.Id, x => x.Ignore());

            CreateMap<HashtagDTO, Hashtag>();
        }
    }
}
