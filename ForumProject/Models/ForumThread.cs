using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumProject.Models
{
    public class ForumThread
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Добавена връзка
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>(); // Добавена връзка
    }
}
