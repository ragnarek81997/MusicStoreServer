using MusicStoreServer.Domain.Entities.Infrastructure;
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
    [Table("Links", Schema = "dbo")]
    public class LinkModel : BaseEntity
    {
        [Required]
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        [Required]
        public string MimeType { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<SongModel> Songs { get; set; }
    }
}
