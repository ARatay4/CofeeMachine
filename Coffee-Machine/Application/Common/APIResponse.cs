using System.Net;

namespace Coffee_Machine.Application.Common
{
    public class APIResponse<T>
    {
        public string? Message { get; set; }
        public string? Prepared { get; set; }
        public T Data { get; set; }

    }
}
