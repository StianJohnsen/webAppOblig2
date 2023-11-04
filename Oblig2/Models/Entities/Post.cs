namespace Oblig2.Models.Entities;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Comment> Comments { get; set; }
    
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}