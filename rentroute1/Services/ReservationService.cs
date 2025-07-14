using rentroute1.Data;
using rentroute1.Models;
using Microsoft.EntityFrameworkCore;

namespace rentroute1.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteReservationAsync(int id)
        //{
        //    var reservation = await _context.Reservations.FindAsync(id);
        //    if (reservation != null)
        //    {
        //        _context.Reservations.Remove(reservation);
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id); // Find reservation by ID
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation); // Remove the reservation
                await _context.SaveChangesAsync(); // Commit changes to the database
            }
        }

    }
}
