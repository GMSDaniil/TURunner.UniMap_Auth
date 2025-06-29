using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Entities
{
    [Table("Places")]
    public class PlaceEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // У базі Contour зберігається як MULTIPOLYGON (string), тож тип string підходить
        public string? Contour { get; set; }
    }
}