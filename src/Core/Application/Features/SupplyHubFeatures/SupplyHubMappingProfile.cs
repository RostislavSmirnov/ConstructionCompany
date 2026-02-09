using Application.DTOs;
using Application.Features.SupplyHubFeatures.CreateSupplyHub;
using AutoMapper;
using Domain.Entities.ConstructionProject;

namespace Application.Features.SupplyHubFeatures;

public class SupplyHubMappingProfile : Profile
{
    public SupplyHubMappingProfile()
    {
        CreateMap<SupplyHub, SupplyHubDTO>()
            .ForMember(dest => dest.Children,
                       opt => opt.MapFrom(src => src.Children)) 
            .ForMember(dest => dest.ParentId,
                       opt => opt.MapFrom(src => src.ParentId));

        CreateMap<SupplyHubDTO, SupplyHub>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BuildingObject, opt => opt.Ignore())
            .ForMember(dest => dest.BuildingObjectId, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.Children, opt => opt.Ignore());

        CreateMap<CreateSupplyHubCommand, SupplyHub>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BuildingObject, opt => opt.Ignore())
            .ForMember(dest => dest.BuildingObjectId, opt => opt.MapFrom(src => src.BuildingObjectId))
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.Children, opt => opt.Ignore());
    }
}
