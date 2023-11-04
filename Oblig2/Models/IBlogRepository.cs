using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models;

public interface IBlogRepository
{
    IEnumerable<Blog> GetAll();
    IEnumerable<Post> GetPostsByBlogId(int id);
    IEnumerable<Comment> GetCommentsByPostId(int id);
    
    BlogViewModel GetBlogViewModel();
    PostViewModel GetPostViewModel();
    
    public void SaveBlog(Blog blog);
    public void SavePost(Post post);

}