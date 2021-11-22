using System;
using System.Net;
using System.Net.Sockets;

namespace ClientServer
{
    internal class MyServer
    {
        private string myIp;
        private int port;

        public MyServer(string myIp, int port)
        {
            this.myIp = myIp;
            this.port = port;

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse(myIp);
           //Статическое свойство Loopback: возвращает объект IPAddress для адреса 127.0.0.1.
           // IPAddress ip = IPAddress.Loopback;
            IPEndPoint ep = new IPEndPoint(ip, port);
            serverSocket.Bind(ep);
            serverSocket.Listen(10);
            byte[] buffer = new byte[1024];
            int myByte;
            string text = "abrakadabra";
            try
            {
              //  Console.WriteLine("Server start " + myIp + ":" + port);
                    int i;
                while (true)
                {
                    Socket ns = serverSocket.Accept();
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    
                    do
                    {
                        i = ns.Receive(buffer);
                        Console.WriteLine(i);
                        text = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                        //  text = System.Text.Encoding.ASCII.GetString(buffer);
                        Console.WriteLine($"text {text}" );
                        text = Console.ReadLine();
                        ns.Send(System.Text.Encoding.ASCII.GetBytes($"Otvet of {ns.RemoteEndPoint.ToString()}" + text));

                    } while (i > 0);
                    ns.Shutdown(SocketShutdown.Both);
                     ns.Close();
                 
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}