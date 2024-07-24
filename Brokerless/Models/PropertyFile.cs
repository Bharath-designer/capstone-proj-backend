using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class PropertyFile
    {
        [Key]
        public int FileId { get; set; }
        public string FileUrl { get; set; }
        public FileType FileType { get; set; }
        public long FileSize { get; set; }
        public int PropertyId { get; set; } // ForeignKey
        public Property Property { get; set; }

    }
}
