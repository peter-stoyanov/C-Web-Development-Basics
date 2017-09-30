namespace HandmadeHttpServer.Server.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Request is not valid.")
            : base(message)
        {
        }
    }
}
