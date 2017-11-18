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
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Business
{
    public class DownloadService : IDownloadService
    {
        public ServiceResult<string> GetFileName(string url)
        {
            var serviceResult = new ServiceResult<string>();
            var uri = new Uri(url);

            if (uri.Segments.Length == 0)
            {
                serviceResult.Success = false;
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Exception of parse url of file.";
                return serviceResult;
            }

            serviceResult.Success = true;
            serviceResult.Result = uri.Segments[uri.Segments.Length - 1];

            return serviceResult;
        }

        public async Task<ServiceResult<byte[]>> GetByteArray(string url)
        {
            var serviceResult = new ServiceResult<byte[]>();

            byte[] byteArray;
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    byteArray = await webClient.DownloadDataTaskAsync(url);
                }
                catch (Exception ex)
                {
                    serviceResult.Success = false;
                    serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                    serviceResult.Error.Description = ex.Message;
                    return serviceResult;
                }
            }

            if(byteArray.Length == 0)
            {
                serviceResult.Success = false;
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "byteArray is null.";
                return serviceResult;
            }

            serviceResult.Success = true;
            serviceResult.Result = byteArray;

            return serviceResult;
        }
    }
}
