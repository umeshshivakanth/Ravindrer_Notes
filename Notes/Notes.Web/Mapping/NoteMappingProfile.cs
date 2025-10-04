using AutoMapper;
using global::Notes.Application.DTOs;
using global::Notes.Web.ViewModels;
using Notes.Domain.Constants;
namespace Notes.Web.Mapping
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<NoteDto, NoteViewModel>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime().ToString(Defaults.ShortDateTime)))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.ModifiedAt.HasValue ? src.ModifiedAt.Value.ToLocalTime().ToString(Defaults.ShortDateTime) : ""));

            CreateMap<NoteViewModel, NoteDto>();
        }
    }
}