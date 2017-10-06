namespace HandmadeHttpServer.Server.Http.Response
{
    using HandmadeHttpServer.Server.Common;
    using Server.Enums;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl) : base()
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpStatusCode.Found;
            this.Headers.Add(new HttpHeader("Location", redirectUrl));
        }
    }
}
