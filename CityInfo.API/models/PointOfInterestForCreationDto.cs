using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public String Name { get; set; } = String.Empty;

        [MaxLength(200)]
        public String? Description { get; set; }
    }
}
