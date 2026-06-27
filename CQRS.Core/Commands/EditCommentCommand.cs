namespace CQRS.Core.Commands;

public class EditCommentCommand:BaseCommand
{
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string Username { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}