using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MusicStoreServer.Services.Interfaces.RealtimeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Business.Hubs
{
    [Authorize]
    [HubName("userActivity")]
    public sealed class UserActivityHub : Hub
    {
        #region Variables

        private static readonly Lazy<UserActivityHub> _instance = new Lazy<UserActivityHub>(() => new UserActivityHub(GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>()));
        public static IHubContext HubContext
        {
            get { return _instance.Value._hubContext; }
        }

        private readonly IHubContext _hubContext;
        private UserActivityHub(IHubContext hubContext)
        {
            this._hubContext = hubContext;
        }


        public ITestRealtimeService _testRealtimeService;

        public UserActivityHub(ITestRealtimeService _testRealtimeService)
        {
            this._testRealtimeService = _testRealtimeService;
        }

        #endregion

        #region Base event
        public override async Task OnConnected()
        {
            //Test code delete
            //var userId = Context.User.Identity.GetUserId();
            //Clients.User(userId).test("Hub was connected!");
            //await _testRealtimeService.Method();

            await base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        //public async Task Test(string test)
        //{
        //    var userId = Context.User.Identity.GetUserId();
        //    Clients.User(userId).testClient(test);
        //    //await _testRealtimeService.Method();
        //}

    }
}
