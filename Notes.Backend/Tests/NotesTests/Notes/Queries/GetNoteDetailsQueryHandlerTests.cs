using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using NotesTests.Common;
using Shouldly;
using Xunit;

namespace NotesTests.Notes.Queries;

[Collection("QueryCollection")]
public class GetNoteDetailsQueryHandlerTests
{
    private readonly NotesDbContext _context;
    private readonly IMapper _mapper;

    public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetNotesDetailsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetNoteDetailsQueryHandler(_context, _mapper);
        //Act
        var result = await handler.Handle(new GetNotesDetailsQuery
            {
                Id = Guid.Parse("D6A83327-5EEB-415C-BB9D-0ADB0435E755"),
                UserId = NotesContextFactory.UserBId
            },
            CancellationToken.None);
        //Arrive
        result.ShouldBeOfType<NoteDetailsVm>();
        result.Title.ShouldBe("Title2");
        result.CreationTime.ShouldBe(DateTime.Today);
    }
}