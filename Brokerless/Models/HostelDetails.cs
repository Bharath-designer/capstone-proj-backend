using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class HostelDetails
    {
        [Key]
        public int HostelId { get; set; }
        public TypesOfRooms TypesOfRooms { get; set; }
        public Gender GenderPreference { get; set; }
        public FoodDetails Food {  get; set; }
        public bool Wifi { get; set; }
        public bool GatedSecurity { get; set; }
        public int PropertyId { get; set; } // Foreign Key
        public Property Property { get; set; }


    }

}
