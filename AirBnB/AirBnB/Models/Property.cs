using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirBnB.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        [Required, MaxLength(200), MinLength(5)]
        public string PropertyTitle { get; set; }

        public string PropertyDescription { get; set; }
        [Required, MinLength(1)]
        public int PropertyCapacity { get; set; }
        [Required, MinLength(1)]
        public int PropertyBedsNum { get; set; }
        [Required, MinLength(1)]
        public int PropertyBedRooms { get; set; }
        [Required, MinLength(1)]
        public int PropertyBath { get; set; }
        [Required]
        public int PropertyPricePerNight { get; set; }
        public string PropertyHostInfo { get; set; }
        [ForeignKey("AppUser")]
        [Required]
        public string AppUserId { get; set; }
        [ForeignKey("Area")]
        [Required]
        public int AreaId { get; set; }
        [ForeignKey("Categoray")]
        [Required]
        public int CategorayId { get; set; }
        //NP
        public AppUser AppUser { get; set; }
        public Area Area { get; set; }
        public Categoray Categoray { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<PropertyImg> PropertyImgs { get; set; } = new HashSet<PropertyImg>();
        public ICollection<Amenity> Amenities { get; set; } = new HashSet<Amenity>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
