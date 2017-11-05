using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Business
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        public async Task<ServiceResult> Add(GenreModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _genreRepository.Add(model);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Delete(string id)
        {
            var serviceResult = new ServiceResult();
            var result = await _genreRepository.Delete(id);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<GenreModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<GenreModel>();
            var result = await _genreRepository.Get(id);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<GenreModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();
            var result = await _genreRepository.GetMany(skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<GenreModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();
            var result = await _genreRepository.GetMany(searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<GenreModel>>> GetMany(List<string> ids, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();
            var result = await _genreRepository.GetMany(ids, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<GenreModel>>> GetMany(List<string> ids, string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();
            var result = await _genreRepository.GetMany(ids, searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Update(GenreModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _genreRepository.Update(model);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }
    }
}
