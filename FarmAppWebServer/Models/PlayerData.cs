using System.Numerics;

namespace FarmAppWebServer.Models
{
    public class PlayerData
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Money { get; set; }
        public int FarmLevel { get; set; }
        public int CurrentPlayerLevel { get; set; }
        public long CurrentPlayerXP { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string LastToken { get; set; }
    }
}
