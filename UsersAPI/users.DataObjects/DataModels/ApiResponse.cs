using System.Net;

namespace users.DataObjects.DataModels
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
