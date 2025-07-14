using Microsoft.AspNetCore.Mvc;
using rentroute1.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace rentroute1.Controllers

{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserReservation()
        {
            var reservations = new List<Reservation>(); // Empty list or fetch reservations if needed
            return View(reservations); // Pass the model to the view
        }






        // POST: Admin/Login
        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Hardcoded admin credentials for username and password
            if (username == "admin" && password == "admin123")
            {
                // Store admin status in TempData
                TempData["AdminLoggedIn"] = true;

                // Redirect to Admin Index page after successful login
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // If login fails, store error message in TempData
                TempData["LoginError"] = "Invalid username or password.";
                return RedirectToAction("Login", "Admin");
            }
        }


















        public IActionResult Dashboard()
        {
            var cars = GetAllCars();
            return View(cars);
        }

        public IActionResult AddCars()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCars(AdminCars model, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                using var ms = new MemoryStream();
                imageFile.CopyTo(ms);
                model.ImageData = Convert.ToBase64String(ms.ToArray());
            }

            AddCarToDatabase(model);
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult ToggleActiveStatus(int id)
        {
            ToggleCarStatus(id);
            return RedirectToAction("Dashboard");
        }

        // Fetch all cars
        private List<AdminCars> GetAllCars()
        {
            var cars = new List<AdminCars>();
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("sp_GetAllCars", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                cars.Add(new AdminCars
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    ImageData = reader["ImageData"].ToString(),
                    Details = reader["Details"].ToString(),
                    ModelType = reader["ModelType"].ToString(),
                    Price = (decimal)reader["Price"],
                    IsActive = (bool)reader["IsActive"]
                });
            }
            return cars;
        }

        // Add car to database
        private void AddCarToDatabase(AdminCars car)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("sp_AddCar", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Title", car.Title);
            command.Parameters.AddWithValue("@ImageData", car.ImageData);
            command.Parameters.AddWithValue("@Details", car.Details);
            command.Parameters.AddWithValue("@ModelType", car.ModelType);
            command.Parameters.AddWithValue("@Price", car.Price);
            connection.Open();
            command.ExecuteNonQuery();
        }

        // Toggle car status
        private void ToggleCarStatus(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("sp_ToggleCarStatus", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
