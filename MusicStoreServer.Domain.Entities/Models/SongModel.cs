using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class SongModel : BaseEntity
    {
        public string Name { get; set; }
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }
        public string GenreId { get; set; }
        public string ArtUrl { get; set; }
        public List<LinkModel> Links { get; set; }
    }
}
