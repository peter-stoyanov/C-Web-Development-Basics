using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int port = 1337;
            string ip = "127.0.0.1";
            IPAddress ipAddress = IPAddress.Parse(ip);

            var tcpListener = new TcpListener(ipAddress, port);

            tcpListener.Start();

            Task.Run(() => ConnectAsync(tcpListener))
                .GetAwaiter()
                .GetResult();
        }

        private static async Task ConnectAsync(TcpListener tcpListener)
        {
            while (true)
            {
                using (TcpClient client = await tcpListener.AcceptTcpClientAsync())
                {
                    // read network request
                    var readBuffer = new byte[1024];
                    await client.GetStream().ReadAsync(readBuffer, 0, readBuffer.Length);

                    // log request to console
                    string clientRequest = Encoding.UTF8.GetString(readBuffer);
                    Console.WriteLine(clientRequest.Trim('\0'));

                    // write network response
                    string response = "HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello from local server.";
                    var writeBuffer = Encoding.UTF8.GetBytes(response);

                    await client.GetStream().WriteAsync(writeBuffer, 0, writeBuffer.Length);
                }
            }
        }
    }
}
