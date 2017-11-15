using MusicStoreServer.Domain.Entities.Dictionaries;
using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicStoreServer.Infrastructure.Data.Utility.AzureBlob
{
    public class UploadSong : UploadBase
    {
        private UploadSongResult uploadSongResult = new UploadSongResult();

        public UploadSong() : this(string.Empty)
        {

        }

        public UploadSong(string userId) : base(userId, ContainerBlob.Songs.ToString().ToLower())
        {
            maxLengthFile = 20000;
        }
        /*-----------------------------------------------------------------*/
        public async Task<UploadSongResult> UploadFileSong(HttpPostedFile inputFile, string path = null)
        {
            try
            {
                string nameFile;

                // If path null save to temporary directory 
                if (path == null)
                    path = UploadSongProperties.TemporaryFolder;
                else
                    path = userId + path;

                if (inputFile == null)
                {
                    uploadSongResult.Error = ErrorDescriptions.FileMissing;

                    return uploadSongResult;
                }

                // Obtaining Content Type 
                string fileType = UploadSongProperties.SongsFolder;

                // Checking filr for errors 
                bool checkFIle = CheckFile(inputFile.InputStream, fileType);

                if (!checkFIle)
                {
                    uploadSongResult.Error += error;

                    return uploadSongResult;
                }

                nameFile = System.Guid.NewGuid().ToString("N").Substring(0, 10); // Giving name of a file 10 symbols 

                // Concatenation of path, file name and type 
                string sourcePath = path + nameFile + fileType;

                uploadSongResult.Id = nameFile;

                // Завантаження потоку на Azure BLOB
                var result = await UploadFileToAzureAsync(inputFile.InputStream, sourcePath, inputFile.ContentType);
                uploadSongResult.Error = result.Error;
                uploadSongResult.PathFile = result.PathFile;

                return uploadSongResult;
            }
            catch (Exception e)
            {
                uploadSongResult.Error += e.Message.ToString();

                return uploadSongResult;
            }
        }

        private bool CheckFile(Stream stream, string mimeType)
        {
            try
            {
                if (stream == null)
                {
                    error = "Stream null";
                    return false;
                }

                if (stream.Length > (maxLengthFile = maxLengthFile * 1024))
                {
                    error = $"Max size: {maxLengthFile}";
                    return false;
                }

                if (mimeType == null)
                {
                    error = "Unknown file";
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                error = ErrorDescriptions.BadType;

                return false;
            }
        }
    }
}
