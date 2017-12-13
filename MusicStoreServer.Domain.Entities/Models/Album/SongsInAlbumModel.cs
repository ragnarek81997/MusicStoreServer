using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreServer.Domain.Entities.Models.Album
{
    [Table("SongsInAlbums")]
    public class SongsInAlbumModel
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [Key, ForeignKey("Album")]
        public string AlbumId { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 24)]
        [Key, ForeignKey("Song")]
        public string SongId { get; set; }

        public virtual AlbumModel Album { get; set; }
        public virtual SongModel Song { get; set; }
    }
}