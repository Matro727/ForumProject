using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        // Полето Content е задължително, така че трябва да бъде попълнено
        [Required]
        public string Content { get; set; } = string.Empty;  // Стойност по подразбиране (празен низ)

        // Има стойност по подразбиране, така че не трябва да се получава предупреждение
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Форумна тема, на която принадлежи този коментар
        [ForeignKey("ForumThread")]
        public int ThreadId { get; set; }

        // Деклариране на ForumThread като nullable
        public ForumThread? ForumThread { get; set; }

        // Потребител, който е публикувал този коментар
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Променено на nullable
        public User? User { get; set; }

        // Събиране на реакции към този коментар
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>(); // Добавена връзка
    }
}
