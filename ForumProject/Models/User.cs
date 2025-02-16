using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsEmailVerified { get; set; } = false;

        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; } = string.Empty;
        public string? VerificationToken { get; set; }


    }
}
