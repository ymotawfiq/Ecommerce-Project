

namespace Ecommerce.Data.Models.ApiModel
{
    public class ApiResponse <T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? ResponseObject { get; set; }
    }
}
