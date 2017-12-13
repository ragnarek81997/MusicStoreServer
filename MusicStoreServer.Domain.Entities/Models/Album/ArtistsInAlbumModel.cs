using MusicStoreServer.Domain.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreServer.Domain.Entities.Models.Album
{
    [Table("ArtistsInAlbums")]
    public class ArtistsInAlbumModel
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [Key, ForeignKey("Album")]
        public string AlbumId { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 24)]
        [Key, ForeignKey("Artist")]
        public string ArtistId { get; set; }

        public virtual AlbumModel Album { get; set; }
        public virtual ArtistModel Artist { get; set; }
    }
}
