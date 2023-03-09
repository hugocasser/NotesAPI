using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using NotesTests.Common;
using Xunit;

namespace NotesTests.Notes.Commands;

public class CreateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateNoteCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateNoteCommandHandler(Context);
        var noteName = "note name";
        var noteDetaisl = "note details";
        
        //Act
        var noteId = await handler.Handle(
            new CreateNoteCommand
            {
                Title = noteName,
                Details = noteDetaisl,
                UserId = NotesContextFactory.UserAId
            },
            CancellationToken.None);
        //Assert
        Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                    note.Id == noteId && note.Title == noteName &&
                    note.Details == noteDetaisl));
    }
}