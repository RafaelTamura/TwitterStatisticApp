using AutoMapper;
using System;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Profiles
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<Language, LanguageDTO>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
            CreateMap<LanguageDTO, Language>().ForMember(x => x.Id, x => x.MapFrom(u => u.Id));
        }
    }
}
