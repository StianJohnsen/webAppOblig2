using System.Security.Principal;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models;

public interface IBlogRepository
{
    IEnumerable<Blog> GetAll();
    IEnumerable<Post> GetPostsByBlogId(int id);
    Post GetPostById(int id);
    Comment GetCommentById(int id);
    IEnumerable<Comment> GetCommentsByPostId(int id);

    BlogViewModel GetBlogViewModel();
    PostViewModel GetPostViewModel();

    PostViewModel GetPostEditViewModelById(int id);

    CommentViewModel GetCommentEditViewModelById(int id);

    CommentViewModel GetCommentViewModel();

    BlogViewModel GetBlogViewModelById(int id);

    public Task SaveBlog(Blog blog, IPrincipal principal);
    public Task SavePost(Post post, IPrincipal principal);
    public void EditPost(Post post);
    public void DeletePost(Post post);
    public void EditComment(Comment comment);
    public Task SaveComment(Comment comment, IPrincipal principal);
    public void DeleteComment(Comment comment);
}