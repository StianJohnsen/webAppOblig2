using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oblig2.Models.Entities;

namespace Oblig2.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Blog> Blog { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<Comment> Comment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Blog>().ToTable("Blog");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Comment>().ToTable("Comment");
        
        // Seeding
        
        modelBuilder.Entity<Blog>().HasData(
            new Blog
            {
                BlogId = 1,
                Name = "Blogg 1",
                Description = "Dette er blogg 1"
            },
            new Blog
            {
                BlogId = 2,
                Name = "Blogg 2",
                Description = "Dette er blogg 2"
            },
            new Blog
            {
                BlogId = 3,
                Name = "Blogg 3",
                Description = "Dette er blogg 3"
            }
        );
        
        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                PostId = 1,
                Title = "Post 1",
                Content = "Dette er post 1",
                BlogId = 1
            },
            new Post
            {
                PostId = 2,
                Title = "Post 2",
                Content = "Dette er post 2",
                BlogId = 1
            },
            new Post
            {
                PostId = 3,
                Title = "Post 3",
                Content = "Dette er post 3",
                BlogId = 2
            },
            new Post
            {
                PostId = 4,
                Title = "Post 4",
                Content = "Dette er post 4",
                BlogId = 2
            },
            new Post
            {
                PostId = 5,
                Title = "Post 5",
                Content = "Dette er post 5",
                BlogId = 3
            },
            new Post
            {
                PostId = 6,
                Title = "Post 6",
                Content = "Dette er post 6",
                BlogId = 3
            }
        );
        
        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                CommentId = 1,
                CommentContent = "Dette er kommentar 1",
                PostId = 1
            },
            new Comment
            {
                CommentId = 2,
                CommentContent = "Dette er kommentar 2",
                PostId = 1
            },
            new Comment
            {
                CommentId = 3,
                CommentContent = "Dette er kommentar 3",
                PostId = 2
            },
            new Comment
            {
                CommentId = 4,
                CommentContent = "Dette er kommentar 4",
                PostId = 2
            },
            new Comment
            {
                CommentId = 5,
                CommentContent = "Dette er kommentar 5",
                PostId = 3
            },
            new Comment
            {
                CommentId = 6,
                CommentContent = "Dette er kommentar 6",
                PostId = 3
            },
            new Comment
            {
                CommentId = 7,
                CommentContent = "Dette er kommentar 7",
                PostId = 4
            },
            new Comment
            {
                CommentId = 8,
                CommentContent = "Dette er kommentar 8",
                PostId = 4
            },
            new Comment
            {
                CommentId = 9,
                CommentContent = "Dette er kommentar 9",
                PostId = 5
            },
            new Comment
            {
                CommentId = 10,
                CommentContent = "Dette er kommentar 10",
                PostId = 5
            },
            new Comment
            {
                CommentId = 11,
                CommentContent = "Dette er kommentar 11",
                PostId = 6
            },
            new Comment
            {
                CommentId = 12,
                CommentContent = "Dette er kommentar 12",
                PostId = 6
            }
        );
    }
}