using Application.DTOs;
using Application.Features.BuildingObjectFeatures.UpdateBuildingObject;
using AutoMapper;
using Domain.Entities.ConstructionProject;

namespace Application.Features.BuildingObjectFeatures;

public class BuildingObjectMappingProfile : Profile
{
    public BuildingObjectMappingProfile()
    {
        CreateMap<BuildingObject, BuildingObjectDTO>()
            .ForMember(dest => dest.SupplyHubs,
                       opt => opt.MapFrom(src => src.SupplyHubs));

        CreateMap<SupplyHub, SupplyHubDTO>()
            .ForMember(dest => dest.Children,
                       opt => opt.MapFrom(src => src.Children))
            .ForMember(dest => dest.ParentId,
                       opt => opt.MapFrom(src => src.ParentId));

        CreateMap<UpdateBuildingObjectCommand, BuildingObject>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())

            .ForMember(dest => dest.Name, opt => opt.Condition(src =>
                   src.Name != null &&
                   !string.IsNullOrWhiteSpace(src.Name)))

            .ForMember(dest => dest.Description, opt => opt.Condition(src =>
                   src.Description != null &&
                   !string.IsNullOrWhiteSpace(src.Description)));

        CreateMap<BuildingObjectDTO, BuildingObject>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SupplyHubs, opt => opt.Ignore());

        CreateMap<SupplyHubDTO, SupplyHub>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BuildingObject, opt => opt.Ignore())
            .ForMember(dest => dest.BuildingObjectId, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.Children, opt => opt.Ignore());
    }
}
