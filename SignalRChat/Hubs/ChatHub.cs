using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub:Hub
    {

        internal static ConcurrentDictionary<string, List<string>> Users = new ConcurrentDictionary<string, List<string>>();
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToCaller(string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", message);
        }
        public Task SendMessageToUser(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
        public async Task JoinGroup(string group)
        {
           
                if (Users.Keys.Any(x => x == group))
            {
                Users[group].Add(Context.ConnectionId);
            }
            else
            {
                Users.TryAdd(group, new List<string>() { Context.ConnectionId});
            }
          
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("UserListCount", Users[group].Count, group);
        }

        public Task SendMessageToGroup(string group,string nickName, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message,group,nickName);
        }

        public override async Task OnConnectedAsync()
        {
            
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
