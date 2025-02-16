using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.UtcNow;
                _context.Posts.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        // ✅ Нов метод за показване на детайлите за конкретен пост
        public IActionResult Details(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            return View(post);
        }
    }
}
