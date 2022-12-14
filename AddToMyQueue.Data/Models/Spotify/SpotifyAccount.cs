using System.ComponentModel.DataAnnotations;

namespace AddToMyQueue.Data.Models.Spotify
{
    public class SpotifyAccount
    {
        [Key]
        [Required]
        public string SpotifyId { get; set; }
        public string? SpotifyUsername { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
