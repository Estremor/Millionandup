﻿using AutoMapper;
using Millionandup.PropertyManagement.Aplication.Dtos;
using Millionandup.PropertyManagement.Domain.Entities;
using static Millionandup.PropertyManagement.Aplication.Dtos.PropertyDataDto;

namespace Millionandup.PropertyManagement.Aplication.Automapper
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Owner
            CreateMap<Owner, OwnerDto>()
                .ForMember(x => x.Photo, src => src.MapFrom(d => d.Photo == null ? string.Empty : Convert.ToBase64String(d.Photo)));

            CreateMap<OwnerDto, Owner>()
                .ForMember(x => x.Photo, src => src.MapFrom(d => string.IsNullOrWhiteSpace(d.Photo) ? null : Convert.FromBase64String(d.Photo)));
            #endregion

            #region Property
            CreateMap<Property, PropertyDataDto>()
                .ForMember(x => x.OwnerDocument, src => src.Ignore())
                .ForMember(x => x.PropertyImages, src => src.Ignore());

            CreateMap<PropertyDataDto, Property>()
                .ForMember(x => x.IdOwner, src => src.Ignore());

            CreateMap<PropertyReadDto, Property>();
            CreateMap<Property, PropertyReadDto>();

            CreateMap<PropertyTraceDto, Property>();
            CreateMap<PropertyTraceDto, PropertyTrace>().ForMember(x => x.Value, src => src.MapFrom(d => d.Value == null || d.Value <= 0 ? d.Price : d.Value));

            #endregion

            #region PropertyImage
            CreateMap<PropertyImage, Image>()
                .ForMember(x => x.File, src => src.MapFrom(d => d.File == null ? string.Empty : Convert.ToBase64String(d.File)))
            .ForMember(x => x.Enabled, src => src.MapFrom(d => d.Enabled));

            CreateMap<Image, PropertyImage>()
                .ForMember(x => x.File, src => src.MapFrom(d => string.IsNullOrWhiteSpace(d.File) ? null : Convert.FromBase64String(d.File)))
                .ForMember(x => x.Enabled, src => src.MapFrom(d => d.Enabled));
            #endregion

            #region Image
            CreateMap<ImageDto, PropertyImage>()
                .ForMember(x => x.File, src => src.MapFrom(d => string.IsNullOrWhiteSpace(d.File) ? null : Convert.FromBase64String(d.File)));

            CreateMap<PropertyImage, ImageDto>()
                .ForMember(x => x.File, src => src.MapFrom(d => d.File == null ? string.Empty : Convert.ToBase64String(d.File)));
            #endregion
        }
    }
}
