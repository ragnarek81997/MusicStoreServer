using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Link;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class SongModel : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }
        [Required]
        public string GenreId { get; set; }
        public string ArtUrl { get; set; }
        [Required]
        public List<LinkModel> Links { get; set; }
    }
}
