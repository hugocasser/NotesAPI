using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;

namespace NotesTests.Common;

public class NotesContextFactory
{
    public static Guid UserAId = Guid.NewGuid();
    public static Guid UserBId = Guid.NewGuid();

    public static Guid NoteIdForDelete = Guid.NewGuid();
    public static Guid NoteIdForUpdate = Guid.NewGuid();

    public static NotesDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new NotesDbContext(options);
        context.Database.EnsureCreated();
        context.Notes.AddRange(
            new Note
            {
                CreationTime = DateTime.Today,
                Details = "Details1",
                EditTime = null,
                Id = Guid.Parse("B7B65233-9512-47DD-9DD6-E6070A599825"),
                Title = "Title1",
                UserId = UserAId
            },
            new Note
            {
                CreationTime = DateTime.Today,
                Details = "Details2",
                EditTime = null,
                Id = Guid.Parse("D6A83327-5EEB-415C-BB9D-0ADB0435E755"),
                Title = "Title2",
                UserId = UserBId
            },
            new Note
            {
                CreationTime = DateTime.Today,
                Details = "Details3",
                EditTime = null,
                Id = NoteIdForDelete,
                Title = "Title3",
                UserId = UserAId
            },
            new Note
            {
                CreationTime = DateTime.Today,
                Details = "Details4",
                EditTime = null,
                Id = NoteIdForUpdate,
                Title = "Title4",
                UserId = UserBId
            }
        );
        context.SaveChanges();
        return context;
    }

    public static void Destroy(NotesDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}