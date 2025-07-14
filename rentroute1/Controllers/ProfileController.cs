using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rentroute1.Data; 
using rentroute1.Models; 

namespace rentroute1.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to fetch and display user data
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users); // Passing users data to the view
        }
    }
}
