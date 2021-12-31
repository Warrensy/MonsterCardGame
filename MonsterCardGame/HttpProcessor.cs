using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class HttpProcessor
    {
        private TcpClient socket;
        private HttpServer httpServer;

        public string Method { get; private set; }
        public string Path { get; private set; }
        public string Version { get; private set; }

        public Dictionary<string, string> Headers { get; }

        public HttpProcessor(TcpClient soc, HttpServer httpServer)
        {
            this.socket = soc;
            this.httpServer = httpServer;

            Method = null;
            Headers = new Dictionary<string, string>();
        }

        public void Process()
        {
            var writer = new StreamWriter(socket.GetStream()) { AutoFlush = true };
            var reader = new StreamReader(socket.GetStream());
            Console.WriteLine();

            // read (and handle) the full HTTP-request
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
                if (line.Length == 0)
                    break;  // empty line means next comes the content (which is currently skipped

                // handle first line of HTTP
                if (Method == null)
                {
                    var parts = line.Split(' ');
                    Method = parts[0];
                    Path = parts[1];
                    Version = parts[2];
                }
                // handle HTTP headers
                else
                {
                    var parts = line.Split(' ');
                    Headers.Add(parts[0].TrimEnd(':'), parts[1]);
                    Console.WriteLine(parts[1]);
                }
            }


            if(Headers.ContainsKey("Content-Length"))
            {
                Console.WriteLine(Headers["Content-Length"]);
                Console.WriteLine("TEST!!!");
                char[] buffer = new char [Convert.ToInt32(Headers["Content-Length"])];
                reader.Read(buffer, 0, Convert.ToInt32(Headers["Content-Length"]));
                Console.WriteLine(buffer);

            }

            // write the full HTTP-response
            string content = $"<html><body><h1>test server</h1>" +
                $"Current Time: {DateTime.Now}" +
                $"<form method=\"GET\" action=\"/form\">" +
                $"<input type=\"text\" name=\"foo\" value=\"foovalue\">" +
                $"<input type=\"submit\" name=\"bar\" value=\"barvalue\">" +
                $"</form></html>";

            Console.WriteLine();
            WriteLine(writer, "HTTP/1.1 200 OK");
            WriteLine(writer, "Server: My simple HttpServer");
            WriteLine(writer, $"Current Time: {DateTime.Now}");
            WriteLine(writer, $"Content-Length: {content.Length}");
            WriteLine(writer, "Content-Type: text/html; charset=utf-8");
            WriteLine(writer, "");
            WriteLine(writer, content);

            writer.WriteLine();
            writer.Flush();
            writer.Close();
        }

        private void WriteLine(StreamWriter writer, string s)
        {
            Console.WriteLine(s);
            writer.WriteLine(s);
        }
    }
}
