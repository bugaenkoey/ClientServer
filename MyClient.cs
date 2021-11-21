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
            Socket MySoket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.IP);

            try
            {
                MySoket.Connect(endPoint);
                Console.WriteLine("Client start " + myIp + ":" + port);
                if (MySoket.Connected)
                {
                    string strSend = "GET\r\n\r\n";
                    MySoket.Send(System.Text.Encoding.ASCII.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = MySoket.Receive(buffer);
                        text = System.Text.Encoding.ASCII.
                        GetString(buffer, 0, l);
                    } while (l > 0);
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