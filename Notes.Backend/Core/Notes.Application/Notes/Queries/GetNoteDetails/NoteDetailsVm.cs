using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class NoteDetailsVm : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? EditTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Note, NoteDetailsVm>()
            .ForMember(noteVm => noteVm.Title,
                opt => opt.MapFrom(note => note.Title))
            .ForMember(noteVm => noteVm.Details,
                opt => opt.MapFrom(note => note.Title))
            .ForMember(noteVm => noteVm.Id,
                opt => opt.MapFrom(note => note.Id))
            .ForMember(noteVm => noteVm.CreationTime,
                opt => opt.MapFrom(note => note.CreationTime))
            .ForMember(noteVm => noteVm.EditTime,
                opt => opt.MapFrom(note => note.EditTime));
    }
}