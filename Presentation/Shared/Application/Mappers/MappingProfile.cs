using AutoMapper;
using Presentation.Features.Security.Command;
using Presentation.Shared.Domain.Entities;
using Presentation.Shared.Extensions;

namespace Presentation.Shared.Application.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password.ComputeSha256Hash()))
            .ForMember(dest => dest.CreateAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateAt, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}