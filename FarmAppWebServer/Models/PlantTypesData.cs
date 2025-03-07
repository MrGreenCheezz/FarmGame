namespace FarmAppWebServer.Models
{
    public class PlantTypesData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StorePrice { get; set; }
        public int HarvestedPrice { get; set; }
        public int SecondsToGrowOneState { get; set; }
        public int MaxGrowState { get; set; }
    }
}
