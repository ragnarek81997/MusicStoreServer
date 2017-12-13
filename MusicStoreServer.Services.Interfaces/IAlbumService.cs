using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Entities.ResultModels;

namespace MusicStoreServer.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<ServiceResult<AlbumModel>> Get(string id);

        Task<ServiceResult<List<AlbumModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<AlbumModel>>> GetMany(string searchQuery, int skip, int take);

        Task<ServiceResult> Add(AlbumResultModel model);
        Task<ServiceResult> Update(AlbumResultModel model);
        Task<ServiceResult> Delete(string id);
    }
}