using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.Id)
            .NotEqual(Guid.Empty);
        RuleFor(deleteNOteCommand => deleteNOteCommand.UserId)
            .NotEqual(Guid.Empty);
    }
}