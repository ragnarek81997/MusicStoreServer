using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class ArtistModel : BaseEntity
    {
        public string Name { get; set; }
        public string ArtUrl { get; set; }
    }
}
