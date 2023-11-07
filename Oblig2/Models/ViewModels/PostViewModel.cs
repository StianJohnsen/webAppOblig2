using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Oblig2.Models.Entities;

namespace Oblig2.Models.ViewModels;

public class PostViewModel
{
    public int PostId { get; set; }
    [StringLength(20)]
    [Required(ErrorMessage = "Tittel må angis")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Innhold må angis")]
    public string Content { get; set; }
    public int BlogId { get; set; }
    public IdentityUser? Owner { get; set; }
    public List<Blog>? Blogs { get; set; }
}