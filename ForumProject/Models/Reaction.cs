using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumProject.Models
{
    public class Reaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } // Например: "Like", "Dislike", "Love"

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("ForumThread")]
        public int? ThreadId { get; set; }
        public ForumThread ForumThread { get; set; }

        [ForeignKey("Comment")]
        public int? CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
