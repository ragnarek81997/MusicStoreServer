using MongoDB.Driver;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Services.Interfaces;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;
using Microsoft.AspNet.Identity;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using MusicStoreServer.Domain.Entities.Enums;
using System.Web.Http;
using MusicStoreServer.Infrastructure.Data.Utility.AzureBlob;
using MusicStoreServer.Domain.Entities.Dictionaries;
using System.Linq;
using AspNet.Identity.MongoDB;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using BencodeNET.Torrents;

namespace MusicStoreServer.Infrastructure.Business
{
    public class TorrentService : ITorrentService
    {
        private readonly IDownloadService _downloadService;

        public TorrentService(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        public async Task<ServiceResult<byte[]>> Create(string url, byte[] byteArray = null, string name = "")
        {
            var serviceResult = new ServiceResult<byte[]>();

            #region byteArray
            if (byteArray == null)
            {
                var byteArrayResult = await _downloadService.GetByteArray(url);
                serviceResult.Success = byteArrayResult.Success;
                if (byteArrayResult.Success)
                {
                    byteArray = byteArrayResult.Result;
                }
                else
                {
                    serviceResult.Error = byteArrayResult.Error;
                    return serviceResult;
                }
            }
            #endregion

            #region name
            if (string.IsNullOrWhiteSpace(name))
            {
                var nameResult = _downloadService.GetFileName(url);
                serviceResult.Success = nameResult.Success;
                if (nameResult.Success)
                {
                    name = nameResult.Result;
                }
                else
                {
                    serviceResult.Error = nameResult.Error;
                    return serviceResult;
                }
            }
            #endregion

            #region pieces
            var pieceSize = RecommendedPieceSize(byteArray.LongLength);
            var pieces = CalcPiecesHash(new byte[1][] { byteArray }, pieceSize);
            #endregion

            #region torrent
            var torrent = new Torrent()
            {
                File = new SingleFileInfo()
                {
                    FileName = name,
                    FileSize = byteArray.LongLength,
                    Md5Sum = GetMD5Hash(byteArray, true)
                },
                Trackers = new List<IList<string>>
                {
                    new List<string>()
                    {
                        "udp://shadowshq.yi.org:6969/announce"
                    },
                    new List<string>()
                    {
                        "udp://tracker.eddie4.nl:6969/announce"
                    },
                    new List<string>()
                    {
                        "udp://tracker.mg64.net:2710/announce"
                    },
                    new List<string>()
                    {
                        "udp://tracker.sktorrent.net:6969"
                    }
                },
                PieceSize = pieceSize,
                Pieces = pieces,
                PiecesAsHexString = ToHex(pieces, true),
                CreatedBy = "MusicStore",
                CreationDate = DateTime.Now,
                Encoding = Encoding.UTF8,
                IsPrivate = false,
                ExtraFields = new BencodeNET.Objects.BDictionary()
            };

            torrent.ExtraFields.Add("url-list", url);
            #endregion

            serviceResult.Success = true;
            serviceResult.Result = torrent.EncodeAsBytes();

            return serviceResult;
        }

        #region Hash
        private string ToHex(byte[] bytes, bool upperCase = false)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        private string GetMD5Hash(byte[] bytes, bool upperCase = false)
        {
            string stringHash = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(bytes);
                stringHash = ToHex(hash, upperCase);
            }
            return stringHash;
        }
        #endregion

        #region Pieces
        private byte[] CalcPiecesHash(byte[][] files, int pieceSize)
        {
            byte[] buffer = new byte[pieceSize];
            int bufferRead = 0;
            long fileRead = 0;
            long overallRead = 0;
            long overallTotal = files.LongLength;
            MD5 md5Hasher = HashAlgoFactory.Create<MD5>();
            SHA1 shaHasher = HashAlgoFactory.Create<SHA1>();
            List<byte> torrentHashes = new List<byte>();

            try
            {
                foreach (byte[] file in files)
                {
                    fileRead = 0;
                    if (md5Hasher != null)
                        md5Hasher.Initialize();

                    while (fileRead < file.Length)
                    {


                        int toRead = (int)Math.Min(buffer.Length - bufferRead, file.Length - fileRead);
                        int read = Read(file, fileRead, buffer, bufferRead, toRead);

                        if (md5Hasher != null)
                            md5Hasher.TransformBlock(buffer, bufferRead, read, buffer, bufferRead);
                        shaHasher.TransformBlock(buffer, bufferRead, read, buffer, bufferRead);

                        bufferRead += read;
                        fileRead += read;
                        overallRead += read;

                        if (bufferRead == buffer.Length)
                        {
                            bufferRead = 0;
                            shaHasher.TransformFinalBlock(buffer, 0, 0);
                            torrentHashes.AddRange(shaHasher.Hash);
                            shaHasher.Initialize();
                        }
                    }
                    if (md5Hasher != null)
                    {
                        md5Hasher.TransformFinalBlock(buffer, 0, 0);
                        md5Hasher.Initialize();
                        //file.MD5 = md5Hasher.Hash;
                    }
                }
                if (bufferRead > 0)
                {
                    shaHasher.TransformFinalBlock(buffer, 0, 0);
                    torrentHashes.AddRange(shaHasher.Hash);
                }
            }
            finally
            {
                if (shaHasher != null)
                    shaHasher.Clear();
                if (md5Hasher != null)
                    md5Hasher.Clear();
            }
            return torrentHashes.ToArray();
        }

        private int Read(byte[] byteArray, long offset, byte[] buffer, int bufferOffset, int count)
        {
            Stream s = new MemoryStream(byteArray);
            if (s.Length < offset + count)
                return 0;
            s.Seek(offset, SeekOrigin.Begin);
            return s.Read(buffer, bufferOffset, count);
        }

        private int RecommendedPieceSize(long totalSize)
        {
            // Check all piece sizes that are multiples of 32kB and
            // choose the smallest piece size which results in a
            // .torrent file smaller than 60kb
            for (int i = 32768; i < 4 * 1024 * 1024; i *= 2)
            {
                int pieces = (int)(totalSize / i) + 1;
                if ((pieces * 20) < (60 * 1024))
                    return i;
            }

            // If we get here, we're hashing a massive file, so lets limit
            // to a max of 4MB pieces.
            return 4 * 1024 * 1024;
        }
        #endregion

        #region Pieces
        private static class HashAlgoFactory
        {
            static Dictionary<Type, Type> algos = new Dictionary<Type, Type>();

            static HashAlgoFactory()
            {
                Register<MD5, MD5CryptoServiceProvider>();
                Register<SHA1, SHA1CryptoServiceProvider>();
            }

            public static void Register<T, U>()
                where T : HashAlgorithm
                where U : HashAlgorithm
            {
                Register(typeof(T), typeof(U));
            }

            public static void Register(Type baseType, Type specificType)
            {
                if (baseType == null) throw new ArgumentNullException("baseType");
                if (specificType == null) throw new ArgumentNullException("specificType");

                lock (algos)
                    algos[baseType] = specificType;
            }

            public static T Create<T>()
                where T : HashAlgorithm
            {
                if (algos.ContainsKey(typeof(T)))
                    return (T)Activator.CreateInstance(algos[typeof(T)]);
                return null;
            }
        }
        #endregion
    }
}