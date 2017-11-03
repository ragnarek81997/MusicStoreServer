using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface ITestModelRepository : IRepository<TestModel>
    {
        Task<TestModel> Test();
    }
}
