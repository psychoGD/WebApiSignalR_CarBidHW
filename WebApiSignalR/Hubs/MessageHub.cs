using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
using WebApiSignalR.Datas;
using WebApiSignalR.Entities;

namespace WebApiSignalR.Hubs
{
    public class MessageHub : Hub
    {
        private Groups _groups { get; set; } = Datas.Groups.Instance;
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message + "'s Offer : ", FileHelper.Read());
        }

        public async Task SendWinnerMessage(string message)
        {
            await Clients.Others.SendAsync("ReceiveInfo", message, FileHelper.Read());
        }

        public async Task JoinRoom(string room, string user)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.OthersInGroup(room).SendAsync("ReceiveJoinInfo", user);
        }
        public async Task JoinRoom2(Room room, string user)
        {
            var c_room = _groups.Rooms.Find((r) => r.name == room.name);
            if (c_room.currentUser < 3)
            {
                c_room.currentUser += 1;
                await Groups.AddToGroupAsync(Context.ConnectionId, room.name);
                await Clients.OthersInGroup(room.name).SendAsync("ReceiveJoinInfo", user);
            }

        }

        public async Task SendMessageRoom(string room, string message)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveMessageRoom", message + "'s Offer : ", FileHelper.Read(room));
        }

        public async Task SendWinnerMessageRoom(string room, string message)
        {

            await Clients.OthersInGroup(room).SendAsync("ReceiveInfoRoom", message, FileHelper.Read(room));
        }


        public async Task DisconnectedUser(string room, string user)
        {
            var c_room = _groups.Rooms.Find((r) => r.name == room);
            c_room.currentUser -= 1;
            await Clients.OthersInGroup(room).SendAsync("ReceiveDisconnectInfo", user);
        }

        public async Task SendChat(string room, string user, string message)
        {
            await Clients.Group(room).SendAsync("GetMessage", user, message);
        }
    }
}
