using AutoMapper;
using System;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.ViewModels;

namespace TwitterStatisticApp.Profiles
{
    public class TweetsByTagProfile : Profile
    {
        public TweetsByTagProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<TweetsByTag, TweetsByTagDTO>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<TweetsByTagDTO, TweetsByTag>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<TweetsByTagDTO, TweetsByTagViewModel>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
        }
    }
}
