using AutoMapper;
using Presentation.Features.Security.Command;
using Presentation.Shared.Domain.Entities;

namespace Presentation.Shared.Application.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}