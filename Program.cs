using System;

namespace ClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string MyIp = "127.0.0.1";
           // string MyIp2 = "127.0.0.2";
            int port = 1024;
            string inSelect = "";
            while (inSelect != "x") {
                Console.WriteLine("(S)erver) / (C)lient) e(X)it ?");

                inSelect = Console.ReadLine().ToLower();
                Console.WriteLine(inSelect);

                if (inSelect=="s")
                {
                    Console.WriteLine("Server");
                    MyClientServer myClientServer =  new MyClientServer(MyIp, port, true, true);
                    myClientServer.StartServer();
                    //new MyServer(MyIp,port);
                }
                if (inSelect == "c")
                {
                    Console.WriteLine("Client");
                    MyClientServer myClientServer = new MyClientServer(MyIp, port, false, true);
                    myClientServer.StartClient();
                    // new MyClient(MyIp,port);
                }
               
            }

        }
    }
}
