using System.ComponentModel.DataAnnotations;

namespace AddToMyQueue.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
