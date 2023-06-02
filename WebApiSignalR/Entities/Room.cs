namespace WebApiSignalR.Entities
{
    public class Room
    {
        public string name { get; set; }
        public int currentUser { get; set; }
        public int limitUser { get; set; } = 3;
        public double currentData { get; set; }
        public double beginData { get; set; }

    }
}
