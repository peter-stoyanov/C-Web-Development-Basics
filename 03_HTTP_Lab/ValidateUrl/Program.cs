using System;
using System.Net;

namespace ValidateUrl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string encodedUrl = Console.ReadLine();

                string decodedUrl = WebUtility.UrlDecode(encodedUrl);

                var uri = new Uri(decodedUrl);

                var protocol = uri.Scheme;
                var host = uri.Host;
                var port = uri.Port;
                var path = uri.AbsolutePath;
                var query = uri.Query;
                var fragment = uri.Fragment;

                bool isValidUrl = true;

                if (String.IsNullOrWhiteSpace(protocol)) { isValidUrl = false; }
                if (String.IsNullOrWhiteSpace(host)) { isValidUrl = false; }

                if (port < 0
                    || (protocol == "http" && port == 443)
                    || (protocol == "https" && port == 80))
                {
                    isValidUrl = false;
                }

                if (String.IsNullOrWhiteSpace(path)) { isValidUrl = false; }

                if (!isValidUrl)
                {
                    Console.WriteLine("Invalid URL");
                }
                else
                {
                    Console.WriteLine($"Protocol: {uri.Scheme}");
                    Console.WriteLine($"Host: {uri.Host}");
                    Console.WriteLine($"Port: {uri.Port}");
                    Console.WriteLine($"Path: {uri.AbsolutePath}");

                    if (!String.IsNullOrWhiteSpace(query)) { Console.WriteLine($"Query: {uri.Query.Substring(1)}"); }
                    if (!String.IsNullOrWhiteSpace(fragment)) { Console.WriteLine($"Fragment: {uri.Fragment.Substring(1)}"); }
                }
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}
