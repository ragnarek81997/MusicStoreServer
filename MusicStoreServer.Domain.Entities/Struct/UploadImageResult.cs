using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Struct
{
    public class UploadImageResult : UploadBaseResult
    {
        public string ResizePathFile { get; set; }
    }
}
