using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Oblig2.Models;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Controllers;

public class BlogController: Controller
{

    private IBlogRepository repository;
    public int BlogId;
    
    
    
    public BlogController(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public ActionResult Index()
    {
        var blogs = repository.GetAll();
        return View(blogs);
    }

    public ActionResult Posts(int id)
    {
        var posts = repository.GetPostsByBlogId(id);
        BlogId = id;
        return View(posts);
    }

    public ActionResult Comments(int id)
    {
        var comments = repository.GetCommentsByPostId(id);
        return View(comments);
    }

    public ActionResult CreatePost()
    {
        var post = repository.GetPostViewModel();
        return View(post);
    }
    
    [HttpPost]
    public ActionResult CreatePost(
        [Bind("Title,Content,BlogId")] PostViewModel postViewModel)
        
    {
        try
        {
            var post = new Post
            {
                PostId = postViewModel.PostId,
                Title = postViewModel.Title,
                Content = postViewModel.Content,
                BlogId = postViewModel.BlogId
            };
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been saved", post.Title);
                repository.SavePost(post);
                return RedirectToAction("Posts");
            }
            else
            {
                return View();
            }
            }
        
        catch
        {
            return View();
        }
    }

    public ActionResult CreateBlog()
    {
        var blog = repository.GetBlogViewModel();
        return View(blog);
    }
    
    [HttpPost]
    public ActionResult CreateBlog(
        [Bind("Name,Description")] BlogViewModel blogViewModel)
        
    {
        try
        {
            var blog = new Blog
            {
                BlogId = blogViewModel.BlogId,
                Name = blogViewModel.Name,
                Description = blogViewModel.Description,
            };
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been saved", blog.Name);
                repository.SaveBlog(blog);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            }
        
        catch
        {
            return View();
        }
    }

}