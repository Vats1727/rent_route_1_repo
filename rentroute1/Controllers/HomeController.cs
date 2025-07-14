using Microsoft.AspNetCore.Mvc;
using rentroute1.Data;
using Microsoft.EntityFrameworkCore;

namespace rentroute1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action for handling Reservations (if needed
        public async Task<IActionResult> ReservationsIndex()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return View(reservations); 
        }

        // Action for handling AdminCars
        public IActionResult Index()
        {
            // Fetch all AdminCars from the database
            var cars = _context.AdminCars.ToList();
            return View(cars); 
        }

        // Admin action (if needed)
        public IActionResult Admin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return Unauthorized();
            }

            return View();
        }
    }
}
