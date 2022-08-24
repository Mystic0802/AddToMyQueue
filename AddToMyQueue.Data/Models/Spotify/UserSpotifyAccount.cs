using System.ComponentModel.DataAnnotations;

namespace AddToMyQueue.Data.Models.Spotify
{
    public class UserSpotifyAccount
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string SpotifyId { get; set; }
    }
}
