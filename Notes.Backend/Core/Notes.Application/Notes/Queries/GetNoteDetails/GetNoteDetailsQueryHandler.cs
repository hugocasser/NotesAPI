using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQueryHandler:
    IRequestHandler<GetNotesDetailsQuery, NoteDetailsVm>
{
    private readonly INotesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);
    public async Task<NoteDetailsVm> Handle(GetNotesDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes
            .FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(entity), request.Id);
        }

        return _mapper.Map<NoteDetailsVm>(entity);
    }
}