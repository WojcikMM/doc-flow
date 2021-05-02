using AutoMapper;
using System;
using DocFlow.Application.Applications.Commands;
using DocFlow.Application.Applications.Queries;
using DocFlow.Web.Shared.Applications;

namespace orkflowManagerMonolith.Web.Server.Domains.Applications
{
    public class ApplicationsProfile : Profile
    {
        public ApplicationsProfile()
        {
            CreateMap<RegisterApplicationDto, CreateApplicationCommand>()
                .ForMember(dest => dest.ApplicationId, ctx => ctx.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ApplicationNumber, ctx => ctx.MapFrom(src => src.ApplicationNumber))
                .ForMember(dest => dest.InitialTransactionId, ctx => ctx.MapFrom(src => src.InitialTransactionId.Value))
                .ForMember(dest => dest.RegistrationUser, ctx => ctx.Ignore());

            CreateMap<GetApplicationQueryDto, GetApplicationsQuery>();

            CreateMap<SearchApplicationQueryDto, GetApplicationsQuery>();

        }
    }
}
