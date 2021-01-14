namespace PlacesAPI.Models
{
    public class PlaceDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public bool IsOpen { get; set; }
    }
}
