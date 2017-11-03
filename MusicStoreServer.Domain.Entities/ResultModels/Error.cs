using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Enums;

namespace MusicStoreServer.Domain.Entities.ResultModels
{
    public class Error
    {
        public ErrorStatusCode Code { get; set; }
        public string Description { get; set; }
    }
}
