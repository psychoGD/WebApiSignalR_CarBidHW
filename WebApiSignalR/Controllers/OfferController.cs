using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WebApiSignalR.Datas;
using WebApiSignalR.Entities;
using WebApiSignalR.Hubs;

namespace WebApiSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private IHubContext<MessageHub> messageHub;
        private Groups _groups { get; set; } = Groups.Instance;
        public OfferController(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
            if (!System.IO.File.Exists("mercedes.txt"))
            {
                FileHelper.Write("mercedes",5000);
            }
            if (!System.IO.File.Exists("chevrolet.txt"))
            {
                FileHelper.Write("chevrolet",1300);
            }
        }
        //[HttpGet("/Rooms")]
        //public List<string> GetRooms()
        //{
        //    List<string> result = new List<string>();
        //    var temp = "";
        //    foreach (var room in _groups.GroupsNames)
        //    {

        //        temp += room;
        //        temp+= ",";
        //        temp += FileHelper.Read(room);
        //        result.Add(temp);
        //        temp = "";
        //    }
        //    return result;
        //}
        [HttpGet("/Rooms")]
        public string GetRooms()
        {
            return JsonConvert.SerializeObject(_groups.Rooms);
        }
        [HttpGet]
        public double Get()
        {
            //messageHub.Clients.All.SendOffersToUser(data);
            return FileHelper.Read();
        }

        [HttpGet("/Room")]
        public double GetRoom(string room)
        {
            //messageHub.Clients.All.SendOffersToUser(data);
            return FileHelper.Read(room);
        }

        [HttpGet("/Increase")]
        public async void Get(double number)
        {
            var data = FileHelper.Read() + number;
            FileHelper.Write(data);
        }
        [HttpGet("/IncreaseRoom")]
        public async void Get(string room,double number)
        {
            var data = FileHelper.Read(room) + number;
            FileHelper.Write(room,data);
        }
        [HttpGet("/IncreaseRoom2")]
        public async void Get2(Room room, double number)
        {
            var data = FileHelper.Read(room.name) + number;
            var c_room = _groups.Rooms.Find((r) => r.name == room.name);
            c_room.currentData = data;
            FileHelper.Write(room.name, data);
        }
    }
}
