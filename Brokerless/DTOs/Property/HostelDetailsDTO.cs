using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.DTOs.Property
{
    public class HostelDetailsDTO: BasePropertyDTO
    {
        [Required]
        public TypesOfRooms? TypesOfRooms { get; set; }
        [Required]
        public Gender? GenderPreference { get; set; }
        [Required]
        public FoodDetails? Food { get; set; }
        [Required]
        public bool? Wifi { get; set; }
        [Required]
        public bool? GatedSecurity { get; set; }
    }
}
