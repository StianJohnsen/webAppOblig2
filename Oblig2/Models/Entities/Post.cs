using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities;

public class Post
{
    public virtual IdentityUser Owner { get; set; }
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Comment> Comments { get; set; }
    
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    public DateTime TimeCreated { get; set; }

}