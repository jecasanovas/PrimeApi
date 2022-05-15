using System.Collections.Generic;
using Courses.API.Errors;

namespace Courses.API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string>? Errors { get; set; }
    }
}