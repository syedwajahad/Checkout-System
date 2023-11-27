using System.Net;

namespace DataObject.Cart.Models
{
    public class ApiResponses<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; } = string.Empty;
        public T? Result { get; set; }
    }
}
