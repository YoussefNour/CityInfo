namespace CityInfo.API.models
{
    public class CityWithoutPointOfInterestDto
    {
        public int Id { get; set; }

        public String Name { get; set; } = String.Empty;

        public String? Description { get; set; }
    }
}