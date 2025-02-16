using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumProject.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ForumThread")]
        public int ThreadId { get; set; }
        public ForumThread ForumThread { get; set; } // Променено от Post на ForumThread

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>(); // Добавена връзка
    }
}
