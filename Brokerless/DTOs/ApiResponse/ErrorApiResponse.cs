namespace Brokerless.DTOs.ApiResponse
{
    public class ErrorApiResponse
    {
        public int ErrCode { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
    }
}
