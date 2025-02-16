using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ForumProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Този имейл вече е регистриран.");
                    return View(model);
                }

                model.Role = "User";
                model.IsEmailVerified = false;
                model.CreatedAt = DateTime.UtcNow;

                // Хеширане на паролата
                model.PasswordHash = HashPassword(model.PasswordHash);

                // Генериране на токен за потвърждение
                model.VerificationToken = Guid.NewGuid().ToString();

                _context.Users.Add(model);
                _context.SaveChanges();

                // Изпращане на имейл
                SendVerificationEmail(model.Email, model.VerificationToken);

                return RedirectToAction("Login");
            }

            return View(model);
        }

        private void SendVerificationEmail(string email, string token)
        {
            try
            {
                var confirmationLink = Url.Action("VerifyEmail", "Auth", new { token = token }, Request.Scheme);
                var message = new MailMessage();
                message.To.Add(email);
                message.Subject = "Потвърдете своя имейл";
                message.Body = $"Кликнете тук, за да потвърдите: {confirmationLink}";
                message.From = new MailAddress("your-email@example.com");

                using (var client = new SmtpClient("smtp.your-email-provider.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("your-email@example.com", "your-password");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при изпращане на имейл: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult VerifyEmail(string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.VerificationToken == token);
            if (user == null)
            {
                return NotFound("Невалиден токен.");
            }

            user.IsEmailVerified = true;
            user.VerificationToken = null;
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || user.PasswordHash != HashPassword(password))
            {
                ModelState.AddModelError(string.Empty, "Невалиден имейл или парола.");
                return View();
            }

            if (!user.IsEmailVerified)
            {
                ModelState.AddModelError(string.Empty, "Имейлът не е потвърден. Проверете входящата си поща.");
                return View();
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
