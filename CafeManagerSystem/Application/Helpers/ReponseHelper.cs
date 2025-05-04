using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ReponseHelper
    {
        public static ResponseApi<T> Success<T>(T data, string message = "Success")
        {
            return new ResponseApi<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = 200 // OK
            };
        }
        public static ResponseApi<T> Fail <T>(string message)
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                StatusCode = 400,
            };
        }
        public static ResponseApi<T> NotFound<T>(string message = "Not found")
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = 404 // Not Found
            };
        }

        public static ResponseApi<T> BadRequest<T>(string message = "Invalid request")
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = 400 // Bad Request
            };
        }

        public static ResponseApi<T> Unauthorized<T>(string message = "Unauthorized access")
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = 401 // Unauthorized
            };
        }

        public static ResponseApi<T> Forbidden<T>(string message = "Forbidden")
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = 403 // Forbidden
            };
        }

        public static ResponseApi<T> ServerError<T>(string message = "Internal server error")
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = 500 // Internal Server Error
            };
        }
    }
}
