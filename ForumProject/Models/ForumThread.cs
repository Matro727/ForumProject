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

        // Промяна на User на nullable
        public User? User { get; set; }  // Променено на nullable тип

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }

        // Конструктор за инициализиране на задължителните стойности
        public ForumThread(string title, string content, int userId)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            UserId = userId;

            // Тук също можеш да зададеш стойност на User, ако имаш такава
            // например:
            // User = someUserObject;

            Comments = new List<Comment>();
            Reactions = new List<Reaction>();
        }
    }
}
