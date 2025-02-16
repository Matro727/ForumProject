using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Показва всички постове
        public IActionResult Index()
        {
            var posts = _context.Posts
                .Include(p => p.User)       // Зарежда автора на поста
                .Include(p => p.Comments)   // Зарежда коментарите
                .ToList();
            return View(posts);
        }

        // Показва детайли за конкретен пост и коментарите му
        public IActionResult Details(int id)
        {
            var post = _context.Posts
                .Include(p => p.User)       // Зарежда автора
                .Include(p => p.Comments)   // Зарежда коментарите
                .ThenInclude(c => c.User)   // Зарежда автора на коментарите
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
    }
}
