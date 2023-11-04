namespace Oblig2.Models.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string CommentContent { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}