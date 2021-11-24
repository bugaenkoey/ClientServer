using System;
using System.Net;
using System.Net.Sockets;

namespace ClientServer
{
    internal class MyClientServer
    {
       public string[] messages =
                 {
            "mne nravitsa s toboy obshatsa",
            "ja hochu tebe skazat'",
            "poslushay mena",
            "vot eto da",
            "a...",
            "m... da",
            "ja hotel skapat'",
            "prikol",
            "vse klasno",
            "Bye",

        };
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        // Статическое свойство Loopback: возвращает объект IPAddress для адреса 127.0.0.1.
        // IPAddress ip = IPAddress.Loopback;
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 1024);
        private byte[] buffer = new byte[1024];
        public bool avto = true;

        public MyClientServer(string myIp, int port, bool avto)
        {
            settings(myIp, port, avto);
        }

        public MyClientServer()
        {
        }

        public void settings(string myIp, int port, bool avto)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(myIp), port);
            this.avto = avto;
        }

        public void StartServer()
        {
            socket.Bind(endPoint);
            socket.Listen(10);

            try
            {
                string message = $"Hi, it is SERVER! {endPoint.Address} : {endPoint.Port}";
                Console.WriteLine($"The server is running!!!\nand listening on the port {endPoint.Address} : {endPoint.Port} ");
                this.socket = socket.Accept();
                Console.WriteLine(socket.RemoteEndPoint.ToString());
                socket.Send(System.Text.Encoding.ASCII.GetBytes(message));
                string he = socket.RemoteEndPoint.ToString();

                Dialog(he);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void StartClient()
        {
            try
            {
                socket.Connect(endPoint);
                if (socket.Connected)
                {
                    string he = "SERVER";

                    Dialog(he);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Dialog(string he)
        {
            int i;
            string stop = "Bye";
            string text;
            string message;

            do
            {
                i = socket.Receive(buffer);
                text = System.Text.Encoding.ASCII.GetString(buffer, 0, i);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{he}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\t{text}");
                Console.ResetColor();

                message = getMessage();
                socket.Send(System.Text.Encoding.ASCII.GetBytes(message));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t{message}");
                Console.ResetColor();

                if (message.Contains(stop) || text.Contains(stop))
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket.Dispose();
                    socket = null;
                    break;
                }
            } while (i > 0);
        }

        private string getMessage()
        {
            if (!avto)
            {
                return Console.ReadLine();
            }
            return getRandomMessage();
        }

        private string getRandomMessage()
        {
            Random rnd = new Random();
            return messages[rnd.Next(0, messages.Length)];
        }
    }
}