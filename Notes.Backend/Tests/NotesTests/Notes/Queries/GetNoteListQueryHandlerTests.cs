using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using NotesTests.Common;
using Shouldly;
using Xunit;

namespace NotesTests.Notes.Queries;

[Collection("QueryCollection")]
public class GetNoteListQueryHandlerTests
{
    private readonly NotesDbContext Context;
    private readonly IMapper Mapper;

    public GetNoteListQueryHandlerTests(QueryTestFixture queryTestFixture)
    {
        Context = queryTestFixture.Context;
        Mapper = queryTestFixture.Mapper;
    }
    [Fact]
    public async void GetNoteListQuery_Success()
    {
        //Arrange
        var handler = new GetNoteListQueryHandler(Context, Mapper);
        //Act
        var result = await handler.Handle(
            new GetNoteListQuery
            {
                UserId = NotesContextFactory.UserBId
            },
            CancellationToken.None);

        //Assert
        result.ShouldBeOfType<NoteListVm>();
        result.Notes.Count.ShouldBe(2);
    }
}