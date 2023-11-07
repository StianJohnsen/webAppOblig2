using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Oblig2.Models;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Controllers;

public class BlogController : Controller
{
    private IBlogRepository repository;
    
    public BlogController(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public ActionResult Index()
    {
        var blogs = repository.GetAll();
        Console.WriteLine(blogs.ToList()[0]);
        return View(blogs);
    }
    
    public ActionResult Posts(int id)
    {
        var posts = repository.GetPostsByBlogId(id);
        return View(posts);
    }

    public ActionResult DetailsBlog(int id)
    {
        var blog = repository.GetBlogById(id);
        return View(blog);
    }

    public ActionResult Comments(int id)
    {
        var comments = repository.GetCommentsByPostId(id);
        return View(comments);
    }

    [Authorize]
    public ActionResult CreateComment()
    {
        var comment = repository.GetCommentViewModel();
        return View(comment);
    }

    [Authorize]
    public ActionResult CreatePost()
    {
        var post = repository.GetPostViewModel();
        return View(post);
    }
    
    [Authorize]
    [HttpPost]
    public ActionResult CreateComment(int id,
        [Bind("CommentContent,CommentId")] CommentViewModel commentViewModel)

    {
        try
        {
            Console.WriteLine(commentViewModel.CommentContent);
            var comment = new Comment
            {
                CommentId = commentViewModel.CommentId,
                CommentContent = commentViewModel.CommentContent,
                PostId = id,
                TimeCreated = DateTime.Now
            };


            if (ModelState.IsValid)
            {
                TempData["message"] = "Your comment has been saved";
                repository.SaveComment(comment, User).Wait();
                return RedirectToAction("Comments",
                    new RouteValueDictionary(new { Controller = "Blog", Action = "Comments", Id = id }));
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

    [Authorize]
    [HttpPost]
    public ActionResult CreatePost(int id,
        [Bind("Title,Content,PostId")] PostViewModel postViewModel)

    {
        try
        {
            Console.WriteLine("BlogId: " + id);
            var post = new Post
            {
                PostId = postViewModel.PostId,
                Title = postViewModel.Title,
                Content = postViewModel.Content,
                BlogId = id,
                TimeCreated = DateTime.Now
            };


            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been saved", post.Title);
                repository.SavePost(post, User).Wait();
                return RedirectToAction("Posts",
                    new RouteValueDictionary(new { Controller = "Blog", Action = "Posts", Id = id }));
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

    [Authorize]
    public ActionResult EditPost(int id)
    {
        var post = repository.GetPostEditViewModelById(id);
        return View(post);
    }

    [Authorize]
    [HttpPost]
    public ActionResult EditPost(int Id,
        [Bind("Title,Content", "BlogId")] PostViewModel postViewModel)

    {
        var post = repository.GetPostById(Id);
        post.Title = postViewModel.Title;
        post.Content = postViewModel.Content;
        post.TimeCreated = DateTime.Now;
        try
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been saved", post.Title);
                repository.EditPost(post,User).Wait();
                return RedirectToAction("Posts",
                    new RouteValueDictionary(new
                        { Controller = "Blog", Action = "Posts", Id = postViewModel.BlogId }));
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

    public ActionResult DetailsPost(int id)
    {
        var post = repository.GetPostEditViewModelById(id);
        return View(post);
    }

    [Authorize]
    public ActionResult DeleteComment(int id)
    {
        var comment = repository.GetCommentById(id);
        repository.DeleteComment(comment, User).Wait();
        TempData["message"] = "Your comment has been deleted";
        return RedirectToAction("Comments",
            new RouteValueDictionary(new { Controller = "Blog", Action = "Comments", Id = comment.PostId }));
    }

    public ActionResult DetailsComment(int id)
    {
        var comment = repository.GetCommentEditViewModelById(id);
        return View(comment);
    }
    
    [Authorize]
    public ActionResult EditComment(int id)
    {
        var comment = repository.GetCommentEditViewModelById(id);
        return View(comment);
    }

    [Authorize]
    [HttpPost]
    public ActionResult EditComment(int Id,
        [Bind("CommentContent", "PostId")] CommentViewModel commentViewModel)

    {
        try
        {
            var comment = repository.GetCommentById(Id);
            comment.CommentContent = commentViewModel.CommentContent;
            comment.TimeCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                TempData["message"] = "Your comment has been edited";
                repository.EditComment(comment, User).Wait();
                return RedirectToAction("Comments",
                    new RouteValueDictionary(new
                        { Controller = "Blog", Action = "Comments", Id = commentViewModel.PostId }));
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

    [Authorize]
    public ActionResult DeletePost(int id)
    {
        var post = repository.GetPostById(id);
        repository.DeletePost(post, User).Wait();
        TempData["message"] = string.Format("{0} was deleted", post.Title);
        return RedirectToAction("Posts",
            new RouteValueDictionary(new { Controller = "Blog", Action = "Posts", Id = post.BlogId }));
    }

    [Authorize]
    public ActionResult CreateBlog()
    {
        var blog = repository.GetBlogViewModel();
        return View(blog);
    }

    [Authorize]
    [HttpPost]
    public ActionResult CreateBlog(
        [Bind("Name,Description","IsOpenForExternalWriters")] BlogViewModel blogViewModel)

    {
        try
        {
            var blog = new Blog
            {
                BlogId = blogViewModel.BlogId,
                Name = blogViewModel.Name,
                Description = blogViewModel.Description,
                TimeCreated = DateTime.Now,
                IsOpenForExternalWriters = blogViewModel.IsOpenForExternalWriters
            };
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been saved", blog.Name);
                repository.SaveBlog(blog, User).Wait();
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