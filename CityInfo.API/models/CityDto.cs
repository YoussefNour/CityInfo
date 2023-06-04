namespace CityInfo.API.models
{
    public class CityDto
    {
        public int Id { get; set; }

        public String Name { get; set; } = String.Empty;

        public String? Description { get; set; }
        public int NumberofPointsOfInterests
        {
            get { return PointsOfInterest.Count; }
        }
        public List<PointOfInterestDto> PointsOfInterest { get; set; } =
            new List<PointOfInterestDto>();
    }
}
