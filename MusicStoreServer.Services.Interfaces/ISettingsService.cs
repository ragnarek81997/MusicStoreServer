using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;

namespace MusicStoreServer.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<IdentityResult> ChangePassword(string userId, ChangePasswordBindingModel model);
    }
}