using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Enums;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<DatabaseOneResult<ApplicationUser>> Get(string id);
    }
}
