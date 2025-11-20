

using System.Net;

namespace MemberService.BO.Responses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(HttpStatusCode statusCode, bool success, string? message = default, T? data = default)
        {
            StatusCode = (int)statusCode;
            Success = success;
            Message = message;
            Data = data;
        }

        public ApiResponse(HttpStatusCode statusCode, bool success, string message)
        {
            this.StatusCode = (int)statusCode;
            Success = success;
            Message = message;
        }

        public static ApiResponse<T> SuccessResponse(T? data = default, string message = "Success")
        {
            return new ApiResponse<T>(HttpStatusCode.OK, true, message, data);
        }

        public static ApiResponse<T> CreatedSuccess(T? data = default, string message = "Created successfully")
        {
            return new ApiResponse<T>(HttpStatusCode.Created, true, message, data);
        }

        public static ApiResponse<T> BadRequest(string message = "Bad Request")
        {
            return new ApiResponse<T>(HttpStatusCode.BadRequest, false, message);
        }

        public static ApiResponse<T> DeleteSuccess(string message = "Deleted successfully")
        {
            return new ApiResponse<T>(HttpStatusCode.NoContent, true, message);
        }

        public static ApiResponse<T> Unauthorized(string message = "Unauthorized")
        {
            return new ApiResponse<T>(HttpStatusCode.Unauthorized, false, message);
        }

        public static ApiResponse<T> Forbidden(string message = "Forbidden")
        {
            return new ApiResponse<T>(HttpStatusCode.Forbidden, false, message);
        }

        public static ApiResponse<T> NotFound(string message = "Not Found")
        {
            return new ApiResponse<T>(HttpStatusCode.NotFound, false, message);
        }

        public static ApiResponse<T> ServerError(string message = "Internal Server Error")
        {
            return new ApiResponse<T>(HttpStatusCode.InternalServerError, false, message);
        }
    }
}