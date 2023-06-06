using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.entities
{
    public class City
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public String Name { get; set; }

        [MaxLength(200)]
        public String? Description { get; set; }
        public List<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();

        public City(String name)
        {
            Name = name;
        }
    }
}
