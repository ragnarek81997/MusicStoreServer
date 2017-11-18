using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class ArtistModel : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(24)]
        public string ArtId { get; set; }
    }
}
