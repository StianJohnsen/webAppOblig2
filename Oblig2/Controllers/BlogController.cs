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
    
    public ActionResult Posts(int blogId, Boolean isOpenId)
    {
        Console.WriteLine("Is open: " + isOpenId);
        var posts = repository.GetPostsByBlogId(blogId);
        return View(posts);
    }

    public ActionResult DetailsBlog(int id)
    {
        var blog = repository.GetBlogById(id);
        return View(blog);
    }

    public ActionResult Comments(int postId, int isOpenId)
    {
        var comments = repository.GetCommentsByPostId(postId);
        return View(comments);
    }

    [Authorize]
    public ActionResult CreateComment(int id)
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
            var post = repository.GetPostById(id);
            Console.WriteLine(commentViewModel.CommentContent);
            var comment = new Comment
            {
                CommentId = commentViewModel.CommentId,
                CommentContent = commentViewModel.CommentContent,
                PostId = id,
                TimeCreated = DateTime.Now,
                Post = post
            };


            if (ModelState.IsValid)
            {
                TempData["message"] = "Your comment has been saved";
                repository.SaveComment(comment, User).Wait();
                return RedirectToAction("Comments", "Blog", new
                {
                    postId = comment.PostId, isOpenId = true
                });
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
        [Bind("Title,Content,PostId", "IsOpenForExternalWriters")] PostViewModel postViewModel)

    {
        try
        {
            var blog = repository.GetBlogById(id);
            Console.WriteLine("BlogId: " + id);
            var post = new Post
            {
                Blog = blog,
                PostId = postViewModel.PostId,
                Title = postViewModel.Title,
                Content = postViewModel.Content,
                BlogId = id,
                TimeCreated = DateTime.Now,
                IsOpenForExternalWriters = postViewModel.IsOpenForExternalWriters
            };


            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been saved", post.Title);
                repository.SavePost(post, User).Wait();
                return RedirectToAction("Posts", "Blog", new
                {
                    blogId = post.BlogId, isOpenId = true
                });
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
        [Bind("Title,Content", "BlogId","IsOpenForExternalWriters")] PostViewModel postViewModel)

    {
        var blog = repository.GetBlogById(postViewModel.BlogId);
        var post = repository.GetPostById(Id);
        post.Title = postViewModel.Title;
        post.Content = postViewModel.Content;
        post.TimeCreated = DateTime.Now;
        post.IsOpenForExternalWriters = postViewModel.IsOpenForExternalWriters;
        try
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} has been edited", post.Title);
                repository.EditPost(post,blog.IsOpenForExternalWriters,User).Wait();
                return RedirectToAction("Posts", "Blog", new
                {
                    blogId = postViewModel.BlogId, isOpenId = true
                });
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
            var post = repository.GetPostById(commentViewModel.PostId);
            var comment = repository.GetCommentById(Id);
            comment.CommentContent = commentViewModel.CommentContent;
            comment.TimeCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                TempData["message"] = "Your comment has been edited";
                repository.EditComment(comment, post.IsOpenForExternalWriters,User).Wait();
                return RedirectToAction("Comments", "Blog", new
                {
                    postId = comment.PostId, isOpenId = true
                });
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
        return RedirectToAction("Posts", "Blog", new
        {
            blogId = post.BlogId, isOpenId = true
        });
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