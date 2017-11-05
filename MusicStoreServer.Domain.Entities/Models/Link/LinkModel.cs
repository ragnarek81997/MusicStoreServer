using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models.Link
{
    public abstract class LinkModel : BaseEntity
    {
        public string OwnerId { get; set; }
    }
}
