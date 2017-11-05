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
    public interface IGenreService
    {
        Task<ServiceResult<GenreModel>> Get(string id);

        Task<ServiceResult<List<GenreModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<GenreModel>>> GetMany(string searchQuery, int skip, int take);

        Task<ServiceResult<List<GenreModel>>> GetMany(List<string> ids, int skip, int take);
        Task<ServiceResult<List<GenreModel>>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<ServiceResult> Add(GenreModel model);
        Task<ServiceResult> Update(GenreModel model);
        Task<ServiceResult> Delete(string id);
    }
}