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
        static Dictionary<string, bool> golden = new Dictionary<string, bool>() {
            {"derkon1",false },
            //
            {"budge1",false },
            {"budge2",false },
            {"budge3",false },
            //
             //
            {"goblin1",false },
            {"goblin2",false },
            {"goblin3",false },
            //
             //
            {"rabbit1",false },
            {"rabbit2",false },
            {"rabbit3",false },
            //
             //
            {"darkknight1",false },
            {"darkknight2",false },
            {"darkknight3",false },
            //
             //
            {"devil1",false },
            {"devil2",false },
            {"devil3",false },

             //
            {"soldier1",false },
            {"soldier2",false },
            {"soldier3",false },
            //
            {"titan1",false },
            {"titan2",false },
            {"titan3",false },
            //
            {"vepar1",false },
            {"vepar2",false },
            {"vepar3",false },
            //
            {"lizard1",false },
            {"lizard2",false },
            {"lizard3",false },
            //
            {"wheel1",false },
            {"wheel2",false },
            {"wheel3",false },
            //
            {"tantalo1",false },
            {"tantalo2",false },
            {"tantalo3",false },
            //
            {"golem1",false },
            {"golem2",false },
            {"golem3",false },
            //
            {"crust1",false },
            {"crust2",false },
            {"crust3",false },
            //


        };
        


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
            golden[tipo + num.ToString()] = state;
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
            Clients.All.SendAsync("goldenIni", golden);
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
