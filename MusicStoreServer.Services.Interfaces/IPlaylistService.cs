using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using MusicStoreServer.Domain.Entities.ResultModels;

namespace MusicStoreServer.Services.Interfaces
{
    public interface IPlaylistService
    {
        Task<ServiceResult<PlaylistModel>> Get(string id);

        Task<ServiceResult<List<PlaylistModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<PlaylistModel>>> GetMany(string searchQuery, int skip, int take);

        Task<ServiceResult> Add(PlaylistResultModel model);
        Task<ServiceResult> Update(PlaylistResultModel model);
        Task<ServiceResult> Delete(string id);
    }
}