using System.ComponentModel.DataAnnotations;

namespace CarparkInfoAPI.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; } 

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; } 
    }
}
