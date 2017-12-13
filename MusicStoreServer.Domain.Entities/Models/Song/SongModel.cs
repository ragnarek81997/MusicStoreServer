using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models.Song
{
    [Table("Songs", Schema = "dbo")]
    public class SongModel : BaseEntity
    {
        public SongModel()
        {
            Artists = new List<ArtistModel>();
            Genres = new List<GenreModel>();
            Links = new List<LinkModel>();
        }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }

        public virtual ICollection<ArtistModel> Artists { get; set; }
        public virtual ICollection<GenreModel> Genres { get; set; }
        public virtual ICollection<LinkModel> Links { get; set; }

        public virtual ICollection<PlaylistModel> Playlists { get; set; }
        public virtual ICollection<AlbumModel> Albums { get; set; }

    }
}
