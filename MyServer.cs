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
            IPEndPoint ep = new IPEndPoint(ip, port);
            serverSocket.Bind(ep);
            serverSocket.Listen(10);
            try
            {
                Console.WriteLine("Server start " + myIp + ":" + port);
                while (true)
                {
                    Socket ns = serverSocket.Accept();
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    string dateTime = DateTime.Now.ToString();
                    Console.WriteLine(dateTime);
                    ns.Send(System.Text.Encoding.ASCII.GetBytes(dateTime));
                    //  ns.Send(System.Text.Encoding.ASCII.GetBytes("Hi!"));
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