using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
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
    Blog GetBlogById(int id);

    public Task SaveBlog(Blog blog, IPrincipal principal);
    public Task SavePost(Post post, IPrincipal principal);
    public Task EditPost(Post post, IPrincipal principal);
    public Task DeletePost(Post post, IPrincipal principal);
    public Task EditComment(Comment comment, IPrincipal principal);
    public Task SaveComment(Comment comment, IPrincipal principal);
    public Task DeleteComment(Comment comment, IPrincipal principal);
}