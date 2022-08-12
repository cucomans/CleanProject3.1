using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Miniblog.Core.Hubs
{
   
    public class OnlineCountHub : Hub
    {
    //    static  Dictionary<string, UserOnline> users = new Dictionary<string, UserOnline>();

    //    private static int Count = 0;
    //    public void Join(string name)
    //    {
            
    //    }
    //    public List<UserOnline> GetUsers()
    //    {
    //        return users.Values.ToList();
    //}
    //    public override Task OnConnectedAsync()
    //    {
    //        Count++;
    //        var connId = this.Context.ConnectionId;
    //        users.Add(connId, new UserOnline(connId, name));
    //        base.OnConnectedAsync();
    //        Clients.All.SendAsync("updateCount", Count,);
    //        return Task.CompletedTask;
    //    }
    //    public override Task OnDisconnectedAsync(Exception exception)
    //    {
    //        var user = users[Context.ConnectionId];
    //        users.Remove(Context.ConnectionId);
    //        Count--;
    //        base.OnDisconnectedAsync(exception);
    //        Clients.All.SendAsync("updateCount", Count);
    //        return Task.CompletedTask;
    //    }
    }
}
