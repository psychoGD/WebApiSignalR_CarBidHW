using WebApiSignalR.Entities;

namespace WebApiSignalR.Datas
{
    public sealed class Groups
    {
        private static Groups instance = null;
        private static readonly object lockObject = new object();

        private Groups()
        {
            //Default Rooms
            GroupsNames = new List<string> {
                "chevrolet",
                "mercedes"
            };
            Rooms = new List<Room>
            {
                new Room {
                name = "chevrolet",
                beginData = 2300,
                currentData = 2300,
                currentUser = 0,
            },
                new Room {
                name = "mercedes",
                beginData = 6300,
                currentData = 6300,
                currentUser = 0,
            }
            };
            users = new List<string>();
        }

        public static Groups Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Groups();
                        }
                    }
                }
                return instance;
            }
        }

        // Other members and methods of the class...
        public List<string> GroupsNames { get; set; }
        public List<Room> Rooms { get; set; }
        public List<string> users { get; set; }
        /// <summary>
        /// This Function Add Room To List And Create A File For Room And Give Default Data 1000
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="data"></param>
        public void CreateRoom(string roomName, double data = 1000)
        {
            if (!File.Exists(roomName))
            {
                FileHelper.Write(roomName, data);
            }
            GroupsNames.Add(roomName);
            var room = new Room
            {
                name = roomName,
                beginData = data,
                currentData = data,
                currentUser = 0,
            };
            Rooms.Add(room);
        }

    }
}
