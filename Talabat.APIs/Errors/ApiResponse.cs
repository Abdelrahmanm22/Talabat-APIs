
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int status,string? message=null)
        {
            this.StatusCode = status;
            this.Message = message ?? GetDefaultMessageForStatusCode(status);
        }

        private string? GetDefaultMessageForStatusCode(int? status)
        {
            return status switch
            {
                400 => "Bad Request",
                401 => "You are not Authorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null,
            };
        }
    }
}
