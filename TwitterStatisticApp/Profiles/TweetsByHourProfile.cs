using AutoMapper;
using System;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.ViewModels;

namespace TwitterStatisticApp.Profiles
{
    public class TweetsByHourProfile : Profile
    {
        public TweetsByHourProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<TweetsByHour, TweetsByHourDTO>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<TweetsByHourDTO, TweetsByHour>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<TweetsByHourDTO, TweetsByHourViewModel>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
        }
    }
}
