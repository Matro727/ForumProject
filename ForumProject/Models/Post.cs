namespace ForumProject.Models
{
    public class Post
    {
        public int Id { get; set; }

        // Промяна на Title, за да е nullable
        public string? Title { get; set; } // Променено на nullable тип

        // Добавяне на стойност по подразбиране
        public string Content { get; set; } = string.Empty; // Празен низ като стойност по подразбиране

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Връзка към автора (User)
        public int UserId { get; set; }

        // Промени на User, за да е nullable
        public User? User { get; set; } // Променено на nullable тип

        // Връзка към коментарите
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
