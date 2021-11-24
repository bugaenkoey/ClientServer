using System;
using System.Net;
using System.Net.Sockets;

namespace ClientServer
{
    internal class MyClientServer
    {
        string[] message =
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
        // private string myIp;
        // private int port;
        // private bool v1;
        private bool avto;
        Socket socket;
        byte[] buffer = new byte[1024];
        IPEndPoint endPoint;

        public MyClientServer(string myIp, int port, bool serer, bool avto)
        {
            // this.myIp = myIp;
            // this.port = port;
            // this.v1 = serer;
            this.avto = avto;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse(myIp);
            //Статическое свойство Loopback: возвращает объект IPAddress для адреса 127.0.0.1.
            // IPAddress ip = IPAddress.Loopback;
            this.endPoint = new IPEndPoint(ip, port);
            if (serer)
            {
                socket.Bind(endPoint);
                socket.Listen(10);
            }
        }

        public void StartServer()
        {
            try
            {

                string message = "Hi it is SERVER!";

                socket = this.socket.Accept();
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
            return message[rnd.Next(0, message.Length)];

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
        public void Dialog(string he)
        {

            string stop = "Bye";
            int i;
            string text;
            string message;
           // ConsoleKey key;
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
                    break;
                }

            } while (i > 0 );
           // Console.ReadKey().Key != ConsoleKey.Escape
        //} while (i > 0 || Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}