using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models.Playlist
{
    [Table("Playlists", Schema = "dbo")]
    public class PlaylistModel : BaseEntity
    {
        public PlaylistModel()
        {
            Songs = new List<SongModel>();
        }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }


        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<SongModel> Songs { get; set; }
    }
}
