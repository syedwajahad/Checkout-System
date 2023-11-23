using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataObject.Cart.Entities
{
    public class ApiResponses<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; } = string.Empty;
        public T? Result { get; set; }
    }
}
