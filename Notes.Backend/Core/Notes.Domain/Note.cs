namespace Notes.Domain;

public class Note
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public string Details { get; set; } 
    public DateTime CreationTime { get; set; }
    public DateTime EditTime { get; set; }
}