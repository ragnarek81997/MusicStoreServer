using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Dictionaries
{
    public static class UploadSongProperties
    {
        public static string TemporaryFolder => Folders["TemporaryFolder"];
        public static string BlobAdress => BlobUrl["BlobAdress"];

        public static string MP3MimeType => "audio/mp3";

        public static string GetType(string mimeType)
        {
            var result = string.Empty;
            try
            {
                result = Types[mimeType];
            }
            catch (Exception)
            {
            }
            return result;
        }

        private static readonly Dictionary<string, string> BlobUrl = new Dictionary<string, string>()
        {
            {"BlobAdress", "https:" + System.Configuration.ConfigurationManager.AppSettings["UrlForAzureBlob"] }
        };


        private static readonly Dictionary<string, string> Folders = new Dictionary<string, string>()
        {
            {"TemporaryFolder", "temporary/" }
        };

        private static readonly Dictionary<string, string> Types = new Dictionary<string, string>()
        {
            { MP3MimeType, ".mp3" }
        };
    }
}
