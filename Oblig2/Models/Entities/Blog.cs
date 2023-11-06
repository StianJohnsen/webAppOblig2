using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities;

public class Blog
{
    public virtual IdentityUser Owner { get; set; }
    public int BlogId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Post> Posts { get; set; }
}