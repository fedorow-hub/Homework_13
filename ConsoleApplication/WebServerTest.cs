

using WebServer;

namespace ConsoleApplication
{
    public static class WebServerTest
    {
        public static void Run()
        {
            var server = new WebServer.WebServer(8080);
            server.RequestReceived += OnRequestReceived;

            server.Start();

            Console.WriteLine("Сервер запущен");
            Console.ReadLine();
        }

        private static void OnRequestReceived(object? sender, RequestReceiverEventArgs e)
        {
            var context = e.Context;
            Console.WriteLine("Connection {0}", context.Request.UserHostAddress);

            using var writer = new StreamWriter(context.Response.OutputStream);

            writer.WriteLine("Hello from Test Web Server-{0}", DateTime.Now);
        }
    }
}
