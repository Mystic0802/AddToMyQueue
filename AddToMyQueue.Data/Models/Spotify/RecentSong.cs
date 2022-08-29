using System.ComponentModel.DataAnnotations;

namespace AddToMyQueue.Data.Models.Spotify
{
    public class AddedSong
    {
        [Key]
        [Required]
        public string SpotifyId { get; set; }
        [Required]
        public string SongUri { get; set; }
    }
}
