using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Dictionaries
{
    public static class UploadImageProperties
    {
        public static string ImageUserFolder => Folders["ImageUserFolder"];
        public static int ImageSmallResize => Resize["ImageSmallResize"];
        public static string BlobAdress => BlobUrl["BlobAdress"];


        private static readonly Dictionary<string, string> BlobUrl = new Dictionary<string, string>()
        {
            {"BlobAdress", "https:" + System.Configuration.ConfigurationManager.AppSettings["UrlForAzureBlob"] }
        };


        private static readonly Dictionary<string, string> Folders = new Dictionary<string, string>()
        {
            {"ImageUserFolder", "profile/" }
        };

        private static readonly Dictionary<string, int> Resize = new Dictionary<string, int>()
        {
            {"ImageSmallResize", 300 }
        };
    }
}
