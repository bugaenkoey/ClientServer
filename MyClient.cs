using System;
using System.Net;
using System.Net.Sockets;

namespace ClientServer
{
    internal class MyClient
    {
        private string myIp;
        private int port;

        public MyClient(string myIp, int port)
        {
            this.myIp = myIp;
            this.port = port;
            string text;
            IPAddress ip = IPAddress.Parse(myIp);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            Socket MySoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            string str = string.Empty;
            while (true)
            {
                try
                {
                    MySoket.Connect(endPoint);
                    Console.WriteLine("Client start " + myIp + ":" + port);
                    if (MySoket.Connected)
                    {
                        byte[] buffer = new byte[1024];
                        int i;
                        do
                        {
                            str = Console.ReadLine();
                            MySoket.Send(System.Text.Encoding.ASCII.GetBytes(str));
                            i = MySoket.Receive(buffer);
                            Console.WriteLine(i);
                            text = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                            //text = System.Text.Encoding.ASCII.GetString(buffer);
                            Console.WriteLine($"Polucheno ot servera {text} ");
                        } while (i > 0);
                        if (str == ".")
                        {
                            Console.WriteLine($"str=={str} text=={text}");
                        }
                        MySoket.Shutdown(SocketShutdown.Both);
                        MySoket.Close();
                    }
                    else
                        Console.WriteLine("Error");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    MySoket.Shutdown(SocketShutdown.Both);
                    MySoket.Close();
                }
            }
        }
    }
}