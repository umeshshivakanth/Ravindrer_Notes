using AutoMapper;
using Notes.Application.DTOs;
using Notes.Domain.Entities;

namespace Notes.Application.Mapping;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<NoteDto, Note>();

        CreateMap<Note, NoteDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedDate));
    }
}
