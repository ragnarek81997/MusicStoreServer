using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Infrastructure.Data;
using MusicStoreServer.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Business
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _applicationDbContext = new ApplicationDbContext();

            _genreRepository = new GenreRepository(_applicationDbContext);
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
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entity;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<GenreModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();
            var result = await _genreRepository.GetMany(skip, take);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entities;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<GenreModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();
            var result = await _genreRepository.GetMany(searchQuery, skip, take);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entities;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
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
