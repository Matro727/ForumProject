namespace ForumProject.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Връзка към автора (User)
        public int UserId { get; set; }
        public User User { get; set; }

        // Връзка към коментарите
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
