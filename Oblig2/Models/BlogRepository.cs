using Microsoft.EntityFrameworkCore;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models;

public class BlogRepository: IBlogRepository
{

    private ApplicationDbContext db;
    
    public BlogRepository(ApplicationDbContext db)
    {
        this.db = db;
    }
    public IEnumerable<Blog> GetAll()
    {
        var blogs = db.Blog
            .ToList();
        return blogs;
    }

    public IEnumerable<Post> GetPostsByBlogId(int id)
    {
        var posts = db.Post
            .Include(b => b.Blog)
            .Where(b => b.BlogId == id)
            .ToList();
        return posts;
    }
    
    public IEnumerable<Comment> GetCommentsByPostId(int id)
    {
        var comments = db.Comment
            .Include(p => p.Post)
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

    public void SaveBlog(Blog blog)
    {
        db.Blog.Add(blog);
        db.SaveChanges();
    }
    
    public void SavePost(Post post)
    {
        db.Post.Add(post);
        db.SaveChanges();
    }
}