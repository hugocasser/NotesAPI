using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Xunit;

namespace NotesTests.Common;

public class QueryTestFixture : IDisposable
{
    public NotesDbContext Context;
    public IMapper Mapper;

    public QueryTestFixture()
    {
        Context = NotesContextFactory.Create();
        var configurationBuilder = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(
                typeof(INotesDbContext).Assembly));
        });
        Mapper = configurationBuilder.CreateMapper();
    }
    
    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}
[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> {}
