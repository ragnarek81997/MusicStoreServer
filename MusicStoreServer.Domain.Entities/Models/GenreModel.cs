using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Entities.Models.Song;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    [Table("Genres", Schema = "dbo")]
    public class GenreModel : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<AlbumModel> Albums { get; set; }
        public virtual ICollection<SongModel> Songs { get; set; }
    }
}
