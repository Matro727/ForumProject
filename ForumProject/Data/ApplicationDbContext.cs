using Microsoft.EntityFrameworkCore;
using ForumProject.Models;

namespace ForumProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ForumThread> Threads { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Post> Posts { get; set; } // Добавихме липсващия DbSet
    }
}
