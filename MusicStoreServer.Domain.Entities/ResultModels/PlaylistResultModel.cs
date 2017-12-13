using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Playlist;
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
    public class PlaylistResultModel : PlaylistModel
    {
        public new ICollection<string> Songs { get; set; }
    }
}
