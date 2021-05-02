using AutoMapper;
using DocFlow.Application.Applications.DTOs;
using DocFlow.Core.Domain;

namespace DocFlow.Infrastructure.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationEntity, ApplicationDto>()
                .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
                .ForMember(x => x.AssignedUserId, x => x.MapFrom(dist => dist.AssignedUserId))
                .ForMember(x => x.AssignedUserName, x => x.Ignore())
                .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
                .ForMember(x => x.LastUpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt))
                .ForMember(x => x.StatusId, x => x.MapFrom(dist => dist.StatusId))
                .ForMember(x => x.StatusName, x => x.Ignore());
        }
    }
}
