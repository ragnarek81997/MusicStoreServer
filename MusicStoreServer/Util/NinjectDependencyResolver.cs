using Ninject;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Infrastructure.Business;
using MusicStoreServer.Infrastructure.Data;
using MusicStoreServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MusicStoreServer.Infrastructure.Business.RealtimeServices;
using MusicStoreServer.Infrastructure.Business.Hubs;
using MusicStoreServer.Services.Interfaces.RealtimeServices;
using MusicStoreServer.Infrastructure.Data.Album;
using MusicStoreServer.Domain.Interfaces.Album;
using MusicStoreServer.Domain.Interfaces.Playlist;
using MusicStoreServer.Domain.Interfaces.Song;
using MusicStoreServer.Infrastructure.Data.Playlist;
using MusicStoreServer.Infrastructure.Data.Song;
using MusicStoreServer.Domain.Entities.Infrastructure;

namespace MusicStoreServer.Web.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //hub
            kernel.Bind<UserActivityHub>().To<UserActivityHub>().InSingletonScope();

            //realtime services
            kernel.Bind<ITestRealtimeService>().To<TestRealtimeService>();

            //configuration

            // repository
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IAlbumRepository>().To<AlbumRepository>();
            kernel.Bind<IArtistRepository>().To<ArtistRepository>();
            kernel.Bind<IGenreRepository>().To<GenreRepository>();
            kernel.Bind<IPlaylistRepository>().To<PlaylistRepository>();
            kernel.Bind<ISongRepository>().To<SongRepository>();
            kernel.Bind<ILinkRepository>().To<LinkRepository>();
            
            // service
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IAccountService>().To<AccountService>();

            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<IArtistService>().To<ArtistService>();
            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<IPlaylistService>().To<PlaylistService>();
            kernel.Bind<ISongService>().To<SongService>();

            kernel.Bind<IDownloadService>().To<DownloadService>();
            kernel.Bind<ITorrentService>().To<TorrentService>();
            
        }
    }
}