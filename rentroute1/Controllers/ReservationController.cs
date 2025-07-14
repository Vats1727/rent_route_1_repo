using Microsoft.AspNetCore.Mvc;
using rentroute1.Models;
using rentroute1.Services;

namespace rentroute1.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationService _service;

        public ReservationController(ReservationService service)
        {
            _service = service;
        }

        // Index action to show all reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _service.GetReservationsAsync();
            return View(reservations);
        }

        // Create action to show the reservation creation form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _service.AddReservationAsync(reservation);
                ViewData["Message"] = "Thank you for your Reservation";
                return View(); // Stay on the same page after form submission
            }
            return View(reservation);
        }


        // Edit action to show reservation details for editing
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _service.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return View(reservation);
        }

        // POst Edit reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _service.UpdateReservationAsync(reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // Delete action to confirm deletion
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _service.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return View(reservation);
        }

        // POST: Delete reservation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
