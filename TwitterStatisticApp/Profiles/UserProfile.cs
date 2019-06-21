using AutoMapper;
using System;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.ViewModels;

namespace TwitterStatisticApp.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<User, UserDTO>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<UserDTO, User>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<UserDTO, UserViewModel>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<UserTweetDTO, UserDTO>()
                .ForMember(x => x.IdStr, x => x.MapFrom(u => u.id_str))
                .ForMember(x => x.ScreenName, x => x.MapFrom(u => u.screen_name))
                .ForMember(x => x.FollowersCount, x => x.MapFrom(u => u.followers_count))
                .ForMember(x => x.Location, x => x.MapFrom(u => u.location))
                .ForMember(x => x.LanguageCode, x => x.MapFrom(u => u.lang));
        }
    }
}
