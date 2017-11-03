using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;

namespace MusicStoreServer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResult> Register(RegisterBindingModel model);
    }
}