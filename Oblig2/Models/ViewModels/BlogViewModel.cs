using System.ComponentModel.DataAnnotations;

namespace Oblig2.Models.ViewModels;

public class BlogViewModel
{
    public int BlogId { get; set; }
    [Required(ErrorMessage = "Blog name must be specified")]
    [StringLength(20)]
    public string Name { get; set; }
    public string Description { get; set; }
}