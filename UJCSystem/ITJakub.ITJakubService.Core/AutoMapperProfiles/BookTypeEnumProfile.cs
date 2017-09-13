﻿using AutoMapper;
using ITJakub.DataEntities.Database.Entities.Enums;
using Vokabular.Shared.DataContracts.Types;

namespace ITJakub.ITJakubService.Core.AutoMapperProfiles
{
    public class BookTypeEnumProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BookTypeEnum, BookTypeEnumContract>().ReverseMap();
            CreateMap<BookTypeEnum, MobileApps.MobileContracts.BookTypeContract>().ReverseMap();
        }
    }
}