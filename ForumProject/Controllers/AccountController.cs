using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Този имейл вече е регистриран.");
                    return View(model);
                }

                model.PasswordHash = _passwordHasher.HashPassword(model, model.PasswordHash);
                model.CreatedAt = DateTime.UtcNow;
                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Грешен имейл или парола.");
                return View();
            }

            return RedirectToAction("Profile", new { id = user.Id });
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // ➤ Нови методи за редактиране на профил:

        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(User model, string? password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.Name = model.Name;
            user.Email = model.Email;

            if (!string.IsNullOrEmpty(password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, password);
            }

            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = user.Id });
        }
    }
}
