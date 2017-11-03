using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Struct
{
    public class UploadBaseResult
    {
        public string Error { get; set; }
        public string PathFile { get; set; }
    }
}
