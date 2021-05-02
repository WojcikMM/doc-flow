using AutoMapper;
using System;
using DocFlow.Application.Statuses.Commands;
using DocFlow.Application.Statuses.Queries;
using DocFlow.Web.Shared.Statuses;

namespace DocFlow.Web.Server.Domains.Statuses
{
    public class StatusesProfile : Profile
    {
        public StatusesProfile()
        {
            CreateMap<GetStatusesQueryDto, GetStatusesQuery>();

            CreateMap<CreateStatusDto, CreateStatusCommand>()
                .ForMember(dest => dest.Id, ctx => ctx.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, ctx => ctx.MapFrom(src => src.Name));

            CreateMap<UpdateStatusDto, UpdateStatusCommand>()
                .ForMember(dest => dest.Name, ctx => ctx.MapFrom(src => src.Name));
        }
    }
}
