using MusicStoreServer.Domain.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreServer.Domain.Entities.Models.Playlist
{
    public class SongsInPlaylistModel : BaseEntity
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [ForeignKey("PlaylistModel")]
        public string PlaylistId { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [ForeignKey("SongModel")]
        public string SongId { get; set; }
    }
}