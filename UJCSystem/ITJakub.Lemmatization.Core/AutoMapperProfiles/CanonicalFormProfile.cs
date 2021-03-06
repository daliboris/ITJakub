using AutoMapper;
using ITJakub.Lemmatization.DataEntities;
using ITJakub.Lemmatization.Shared.Contracts;

namespace ITJakub.Lemmatization.Core.AutoMapperProfiles
{
    public class CanonicalFormProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CanonicalForm, CanonicalFormContract>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Text))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
                .Include<CanonicalForm, CanonicalFormTypeaheadContract>()
                .Include<CanonicalForm, InverseCanonicalFormContract>();

            CreateMap<CanonicalForm, CanonicalFormTypeaheadContract>()
                .ForMember(dest => dest.HyperCanonicalForm, opts => opts.MapFrom(src => src.HyperCanonicalForm));

            CreateMap<CanonicalForm, InverseCanonicalFormContract>()
                .ForMember(dest => dest.CanonicalFormFor, opts => opts.MapFrom(src => src.CanonicalFormFor));

            CreateMap<CanonicalFormType, CanonicalFormTypeContract>()
                .ReverseMap();
        }
    }
}