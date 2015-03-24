﻿using System.Linq;
using AutoMapper;
using ITJakub.DataEntities.Database.Entities;
using ITJakub.Shared.Contracts;

namespace ITJakub.ITJakubService.Core.AutoMapperProfiles
{
    public class BookProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Book, BookContract>()
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.BookVersions.First().Title))
                .ForMember(m => m.SubTitle, opt => opt.MapFrom(src => src.BookVersions.First().SubTitle));
        }
    }
}