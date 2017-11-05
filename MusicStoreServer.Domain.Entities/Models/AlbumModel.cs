using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class AlbumModel : BaseEntity
    {
        public string Name { get; set; }
        public string ArtUrl { get; set; }
        public string ArtistId { get; set; }
        public string GenreId { get; set; }
    }
}
