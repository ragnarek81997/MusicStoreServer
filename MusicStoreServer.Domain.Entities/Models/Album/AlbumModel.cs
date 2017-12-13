using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models.Album
{
    [Table("Albums", Schema = "dbo")]
    public class AlbumModel : BaseEntity
    {
        public AlbumModel()
        {
            Artists = new List<ArtistModel>();
            Genres = new List<GenreModel>();
            Songs = new List<SongModel>();
        }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }
        [Required]
        public virtual ICollection<ArtistModel> Artists { get; set; }
        [Required]
        public virtual ICollection<GenreModel> Genres { get; set; }
        public virtual ICollection<SongModel> Songs { get; set; }
    }
}
