using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Infrastructure
{
    public interface IBaseEntity
    {
        [Key]
        [StringLength(24, MinimumLength = 24)]
        string Id { get; set; }
    }
}
