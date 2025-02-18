using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Carpark
    {
        [Key]
        public required string car_park_no { get; set; } 
        public required string address { get; set; }
        public required float x_coord { get; set; }
        public required float y_coord { get; set; }
        public required string car_park_type { get; set; }
        public required string type_of_parking_system { get; set; }
        public required string short_term_parking { get; set; }
        public required string free_parking { get; set; }
        public required bool night_parking { get; set; }
        public required int car_park_decks { get; set; }
        public required float gantry_height { get; set; }
        public required bool car_park_basement { get; set; }
    }

}
