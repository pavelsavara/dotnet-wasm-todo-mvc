namespace TodoMVC;

public record Todo
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public bool? Completed { get; set; }
}
