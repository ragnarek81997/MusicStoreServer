using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Struct
{
    public class UploadSongResult : UploadBaseResult
    {
        public string Id { get; set; }
        public string MimeType { get; set; }
    }
}
