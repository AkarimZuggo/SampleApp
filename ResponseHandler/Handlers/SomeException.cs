using System;
using System.Globalization;

namespace Infrastructure.ResponseHandler.Handlers
{
    public class SomeException : Exception
    {
        public SomeException() : base()
        {
        }

        public SomeException(string message) : base(message)
        {
        }

        public SomeException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
