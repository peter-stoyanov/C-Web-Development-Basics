namespace HandmadeHttpServer.Application.Views.Home
{
    using HandmadeHttpServer.Server.Contracts;

    public class HomeIndexView : IView
    {
        public string View()
        {
            return "<h1>Welcome</h1>";
        }
    }
}
