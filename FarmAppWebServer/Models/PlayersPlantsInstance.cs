namespace FarmAppWebServer.Models
{
    public class PlayersPlantsInstance
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public DateTime LastClientInteraction { get; set; }
        public int GrowStateInSeconds { get; set; }
        public int PotIndex { get; set; }
        public int CurrentGrowState {  get; set; }
        public int PlantType { get; set; }
    }
}
