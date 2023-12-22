using System;

namespace Courses.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = "")
        {
            StatusCode = statusCode;
            if (message == String.Empty)
            {
                Message = GetDefaultMessageForStatusCode(statusCode);
            }

        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Nout Authorized",
                404 => "Resource not Found",
                500 => "Server error",
                _ => String.Empty
            };
        }
    }
}