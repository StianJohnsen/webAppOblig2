using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models;

public class BlogRepository: IBlogRepository
{

    private ApplicationDbContext db;
    private UserManager<IdentityUser> manager;
    
    public BlogRepository(UserManager<IdentityUser> userManager,ApplicationDbContext db)
    {
        manager = userManager;
        this.db = db;
    }
    public IEnumerable<Blog> GetAll()
    {
        var blogs = db.Blog
            .Include(b => b.Owner)
            .ToList();
        return blogs;
    }

    public IEnumerable<Post> GetPostsByBlogId(int id)
    {
        var posts = db.Post
            .Include(p => p.Blog)
            .Include(p => p.Owner)
            .Where(p => p.BlogId == id)
            .ToList();
        return posts;
    }

    public Post GetPostById(int id)
    {
        var post = db.Post.Find(id);
        return post;
    }

    public Comment GetCommentById(int id)
    {
        var comment = db.Comment.Find(id);
        return comment;
    }


    public IEnumerable<Comment> GetCommentsByPostId(int id)
    {
        var comments = db.Comment
            .Include(c => c.Owner)
            .Include(c => c.Post)
            .Where(p => p.PostId == id)
            .ToList();
        return comments;
    }

    public BlogViewModel GetBlogViewModel()
    {
        var viewModel = new BlogViewModel();
        return viewModel;
    }
    
    public PostViewModel GetPostViewModel()
    {
        var viewModel = new PostViewModel();
        return viewModel;
    }

    public PostViewModel GetPostEditViewModelById(int id)
    {
        var post = db.Post.Find(id);
        var viewModel = new PostViewModel
        {
            PostId = post.PostId,
            BlogId = post.BlogId,
            Content = post.Content,
            Title = post.Title,
        };
        return viewModel;
    }

    public CommentViewModel GetCommentEditViewModelById(int id)
    {
        var comment = db.Comment.Find(id);
        var viewModel = new CommentViewModel
        {
            CommentId = id,
            CommentContent = comment.CommentContent,
            PostId = comment.PostId
        };
        return viewModel;
    }

    public CommentViewModel GetCommentViewModel()
    {
        var viewModel = new CommentViewModel();
        return viewModel;
    }

    public BlogViewModel GetBlogViewModelById(int id)
    {
        var blog = db.Blog.Find(id);
        var viewModel = new BlogViewModel
        {
            BlogId = blog.BlogId,
            Name = blog.Name,
            Description = blog.Description,
            Owner = blog.Owner
        };
        return viewModel;
    }



    public async Task SaveBlog(Blog blog, IPrincipal principal)
    {
        var user = await manager.FindByNameAsync(principal.Identity.Name);
        blog.Owner = user;
        db.Blog.Add(blog);
        db.SaveChanges();
    }

    
    public async Task SavePost(Post post, IPrincipal principal)
    {
        var user = await manager.FindByNameAsync(principal.Identity.Name);
        post.Owner = user;
        db.Post.Add(post);
        db.SaveChanges();
    }
    
    public async Task SaveComment(Comment comment , IPrincipal principal)
    {
        var user = await manager.FindByNameAsync(principal.Identity.Name);
        comment.Owner = user;
        db.Comment.Add(comment);
        db.SaveChanges();
    }

    
    public void EditPost(Post post)
    {
        db.Post.Update(post);
        db.SaveChanges();
    }

    public void DeletePost(Post post)
    {
        db.Post.Remove(post);
        db.SaveChanges();
    }

    public void EditComment(Comment comment)
    {
        db.Comment.Update(comment);
        db.SaveChanges();
    }



    public void DeleteComment(Comment comment)
    {
        db.Comment.Remove(comment);
        db.SaveChanges();
    }
}