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
            {"budge4",false },
            {"budge5",false },
            //
             //
            {"goblin1",false },
            {"goblin2",false },
            {"goblin3",false },
            {"goblin4",false },
            {"goblin5",false },
            //
             //
            {"rabbit1",false },
            {"rabbit2",false },
            {"rabbit3",false },
            {"rabbit4",false },
            {"rabbit5",false },
            //
             //
            {"darkknight1",false },
            {"darkknight2",false },
            {"darkknight3",false },
            {"darkknight4",false },
            {"darkknight5",false },
            //
             //
            {"devil1",false },
            {"devil2",false },
            {"devil3",false },
            {"devil4",false },
            {"devil5",false },

             //
            {"soldier1",false },
            {"soldier2",false },
            {"soldier3",false },
            {"soldier4",false },
            {"soldier5",false },
            //
            {"titan1",false },
            {"titan2",false },
            {"titan3",false },
            {"titan4",false },
            {"titan5",false },
            //
            {"vepar1",false },
            {"vepar2",false },
            {"vepar3",false },
            {"vepar4",false },
            {"vepar5",false },
            //
            {"lizard1",false },
            {"lizard2",false },
            {"lizard3",false },
            {"lizard4",false },
            {"lizard5",false },
            //
            {"wheel1",false },
            {"wheel2",false },
            {"wheel3",false },
            {"wheel4",false },
            {"wheel5",false },
            //
            {"tantalo1",false },
            {"tantalo2",false },
            {"tantalo3",false },
            {"tantalo4",false },
            {"tantalo5",false },
            //
            {"golem1",false },
            {"golem2",false },
            {"golem3",false },
            {"golem4",false },
            {"golem5",false },
            //
            {"satyro1",false },
            {"satyro2",false },
            {"satyro3",false },
            {"satyro4",false },
            {"satyro5",false },
            //
            {"twintale1",false },
            {"twintale2",false },
            {"twintale3",false },
            {"twintale4",false },
            //
            {"napin1",false },
            {"napin2",false },
            {"napin3",false },
            {"napin4",false },
            //
            {"ironknight1",false },
            {"ironknight2",false },
            {"ironknight3",false },
            {"ironknight4",false },
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
