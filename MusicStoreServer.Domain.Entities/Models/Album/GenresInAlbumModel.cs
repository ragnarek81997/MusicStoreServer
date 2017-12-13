using MusicStoreServer.Domain.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreServer.Domain.Entities.Models.Album
{
    [Table("GenresInAlbums")]
    public class GenresInAlbumModel
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [Key, ForeignKey("Album")]
        public string AlbumId { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 24)]
        [Key, ForeignKey("Genre")]
        public string GenreId { get; set; }

        public virtual AlbumModel Album { get; set; }
        public virtual GenreModel Genre { get; set; }
    }
}
