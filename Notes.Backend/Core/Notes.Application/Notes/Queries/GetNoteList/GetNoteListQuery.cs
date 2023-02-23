using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQuery : IRequest<NoteLIstVm>
{
    public Guid UserId { get; set; }
}