using Notes.Domain;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Notes.Application.Interfaces;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}