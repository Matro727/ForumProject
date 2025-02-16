using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class Reaction
    {
        [Key]
        public int Id { get; set; }

        // Задължително поле Type с дефинирана стойност
        [Required]
        public string Type { get; set; } = string.Empty; // Празен низ като стойност по подразбиране

        [ForeignKey("User")]
        public int UserId { get; set; }

        // Деклариране на User като nullable
        public User? User { get; set; }

        // Може да бъде null, защото не всички реакции ще бъдат към тема
        [ForeignKey("ForumThread")]
        public int? ThreadId { get; set; }

        // Форумна тема с nullable тип
        public ForumThread? ForumThread { get; set; } // Nullable тип, може да бъде null

        // Може да бъде null, защото не всички реакции ще бъдат към коментари
        [ForeignKey("Comment")]
        public int? CommentId { get; set; }
        public Comment? Comment { get; set; } // Променено на nullable
    }
}
