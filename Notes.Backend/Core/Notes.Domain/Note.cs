namespace Notes.Domain;

public class Note
{
    public Guid UserId { get; set; }
    public Guid NoteId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public DateTime EditTime { get; set; }
}