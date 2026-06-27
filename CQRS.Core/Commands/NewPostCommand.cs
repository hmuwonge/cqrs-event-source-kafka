namespace CQRS.Core.Commands;

public class NewPostCommand:BaseCommand
{
    public string Author { get; set; }
    public string Message { get; set; }
    // public string Content { get; set; }
}