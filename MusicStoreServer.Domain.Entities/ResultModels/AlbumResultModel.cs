using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.ResultModels
{
    [NotMapped]
    public class AlbumResultModel : AlbumModel
    {
        [Required]
        public new ICollection<string> Artists { get; set; }
        [Required]
        public new ICollection<string> Genres { get; set; }

        public new ICollection<string> Songs { get; set; }
    }
}
