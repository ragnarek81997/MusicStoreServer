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
    public interface IArtistService
    {
        Task<ServiceResult<ArtistModel>> Get(string id);

        Task<ServiceResult<List<ArtistModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<ArtistModel>>> GetMany(string searchQuery, int skip, int take);

        Task<ServiceResult<List<ArtistModel>>> GetMany(List<string> ids, int skip, int take);
        Task<ServiceResult<List<ArtistModel>>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<ServiceResult> Add(ArtistModel model);
        Task<ServiceResult> Update(ArtistModel model);
        Task<ServiceResult> Delete(string id);
    }
}