using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNotesDetailsQueryValidator : AbstractValidator<GetNotesDetailsQuery>
{
    public GetNotesDetailsQueryValidator()
    {
        RuleFor(note => note.Id)
            .NotEqual(Guid.Empty);
        RuleFor(note => note.UserId)
            .NotEqual(Guid.Empty);
    }
}