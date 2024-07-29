using Brokerless.Enums;

namespace Brokerless.DTOs.Property
{
    public class FileReturnDTO
    {
        public int FileId { get; set; }
        public string FileUrl { get; set; }
        public FileType FileType { get; set; }
        public long FileSize { get; set; }
    }
}
