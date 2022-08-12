using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miniblog.Core.Hubs
{
    public class UserOnline
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public UserOnline(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
 

    public class ChatHub : Hub
    {
        private static int Count = 0;

        static Dictionary<string, UserOnline> users = new Dictionary<string, UserOnline>();

        public string ReturnUsersToString(Dictionary<string,UserOnline> users)
        {
            var aux = "";
            foreach (var r in users)
            {
                aux += r.Value.Name + " | ";
            }
            return aux;
        }
        public async Task Send(string name, int num, string tipo, bool state)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, num, tipo, state);
        }
        public async Task AddUser(string name)
        {
            Count++;
            var connId = this.Context.ConnectionId;
            users.Add(connId, new UserOnline(connId, name));
            await Clients.All.SendAsync("updateCount",users.Count.ToString() + " - " + ReturnUsersToString(users));
        }
        public override Task OnConnectedAsync()
        {
            
            return Task.CompletedTask;
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = users[Context.ConnectionId];
            users.Remove(Context.ConnectionId);
            Count--;
            base.OnDisconnectedAsync(exception);
            Clients.All.SendAsync("updateCount", ReturnUsersToString(users));
            return Task.CompletedTask;
        }
    }
}
