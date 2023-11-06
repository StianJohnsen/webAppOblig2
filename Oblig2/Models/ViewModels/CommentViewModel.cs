using System.ComponentModel.DataAnnotations;
using Oblig2.Models.Entities;


namespace Oblig2.Models.ViewModels;

public class CommentViewModel
{
    public int CommentId { get; set; }
    [Required(ErrorMessage = "Comment must be specified")]
    public string CommentContent { get; set; }
    public int PostId { get; set; }
    public List<Post>? Posts { get; set; }
    
}