using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Entities.Models;

namespace MusicStoreServer.Services.Interfaces
{
    public interface IPlaylistService
    {
        Task<ServiceResult<PlaylistModel>> Get(string id);

        Task<ServiceResult<List<PlaylistModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<PlaylistModel>>> GetMany(string searchQuery, int skip, int take);

        Task<ServiceResult> Add(PlaylistModel model);
        Task<ServiceResult> Update(PlaylistModel model);
        Task<ServiceResult> Delete(string id);
    }
}