using Courses.API.Errors;

namespace Courses.API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = "", string details = "") : base(statusCode, message ?? "")
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}