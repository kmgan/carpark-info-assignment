using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Carpark
    {
        [Key]
        [Required]
        public string car_park_no { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public float x_coord { get; set; }

        [Required]
        public float y_coord { get; set; }

        [Required]
        public string car_park_type { get; set; }

        [Required]
        public string type_of_parking_system { get; set; }

        [Required]
        public string short_term_parking { get; set; }

        [Required]
        public string free_parking { get; set; }

        [Required]
        public bool night_parking { get; set; }

        [Required]
        public int car_park_decks { get; set; }

        [Required]
        public float gantry_height { get; set; }

        [Required]
        public bool car_park_basement { get; set; }
    }
}
