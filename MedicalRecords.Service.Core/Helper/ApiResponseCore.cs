
namespace MedicalRecords.Service.Core
{
    public class ApiResponseCore
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponseCore(int statusCode , string? message = null) 
        {
            StatusCode = statusCode ;
            Message = message ?? GetDefaultForErrorResponse(statusCode);
        }

        private string GetDefaultForErrorResponse(int statusCode)
        {
            return statusCode switch
            {
                404 => "Error 404: Not Found.",
                500 => "Error 500: Internal Server Error.",
                401 => "Error 401: Unauthorized.",
                403 => "Error 403: Forbidden.",
                400 => "Error 400: Bad Request.",
                _ => "Unknown error code."
            };
        }
    }
}
