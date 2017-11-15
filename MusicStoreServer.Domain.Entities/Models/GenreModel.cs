using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class GenreModel : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
