using System.ComponentModel.DataAnnotations;

namespace rentroute1.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string PickupLocation { get; set; }

        [Required]
        public DateTime PickupDate { get; set; }

        [Required]
        public TimeSpan PickupTime { get; set; }

        [Required]
        public DateTime DropDate { get; set; }

        [Required]
        public TimeSpan DropTime { get; set; }
    }
}

