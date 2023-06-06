using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public String Name { get; set; } = String.Empty;

        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }

        public PointOfInterest(String name)
        {
            Name = name;
        }
    }
}
