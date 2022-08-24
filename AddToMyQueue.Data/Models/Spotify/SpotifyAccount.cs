using System.ComponentModel.DataAnnotations;

namespace AddToMyQueue.Data.Models.Spotify
{
    public class SpotifyAccount
    {
        [Key]
        [Required]
        public string SpotifyId { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public DateTime TokenCreationTime { get; set; }
    }
}
