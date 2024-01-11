using System.Net;

namespace Millionandup.PropertyManagement.Aplication.Errors
{
    public sealed class RestException : Exception
    {
        public readonly HttpStatusCode Code;
        public readonly object Errors;
        public RestException(HttpStatusCode code, object errors = null)
        {
            this.Code = code;
            this.Errors = errors;
        }
    }
}
