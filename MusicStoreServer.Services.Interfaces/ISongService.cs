using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Entities.ResultModels;

namespace MusicStoreServer.Services.Interfaces
{
    public interface ISongService
    {
        Task<ServiceResult<SongModel>> Get(string id);

        Task<ServiceResult<List<SongModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<SongModel>>> GetMany(string searchQuery, int skip, int take);

        Task<ServiceResult> Add(SongResultModel model);
        Task<ServiceResult> Update(SongResultModel model);
        Task<ServiceResult> Delete(string id);
    }
}