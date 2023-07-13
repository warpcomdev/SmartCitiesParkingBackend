using System;

namespace SCParking.Domain.Validation
{
    public class InvalidDynamicCommandException : Exception
    {
        public dynamic ResponseOperation { get; }
        public InvalidDynamicCommandException(dynamic response)
        {
            this.ResponseOperation = response;
        }
    }
}
