using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarparkInfoAPI.Models
{
    public class UserFavourite
    {
        [Key]
        public int user_favourite_id { get; set; } 

        [Required]
        [ForeignKey("User")]
        public int user_id { get; set; } 

        [Required]
        [ForeignKey("Carpark")]
        public string car_park_no { get; set; } 
    }
}
