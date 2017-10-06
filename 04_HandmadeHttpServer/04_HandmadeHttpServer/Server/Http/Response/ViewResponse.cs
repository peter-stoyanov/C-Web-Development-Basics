namespace HandmadeHttpServer.Server.Http.Response
{
    using HandmadeHttpServer.Server.Exceptions;
    using Server.Contracts;
    using Server.Enums;

    public class ViewResponse : HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode statusCode, IView view) : base()
        {
            ValidateStatusCode(statusCode);

            this.view = view;
            this.StatusCode = statusCode;
        }

        private static void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;
            if (299 < statusCodeNumber && statusCodeNumber < 400)
            {
                throw new InvalidResponseException("View responses need status code below 300 or above 400.");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}{this.view.View()}";
        }
    }
}
