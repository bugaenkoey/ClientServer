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
                    Console.WriteLine("Server (A)avto (H) hand ?");
                    inSelect = Console.ReadLine().ToLower();
                    bool avto = true;
                    
                    if (inSelect == "h")
                    {
                        avto = false;
                    }

                    MyClientServer myClientServer =  new MyClientServer(MyIp, port, true, avto);
                    myClientServer.StartServer();
                }
                if (inSelect == "c")
                {
                    Console.WriteLine("Client (A)avto (H) hand ?");
                    inSelect = Console.ReadLine().ToLower();
                    bool avto = true;

                    if (inSelect == "h")
                    {
                        avto = false;
                    }

                    MyClientServer myClientServer = new MyClientServer(MyIp, port, false, avto);
                    myClientServer.StartClient();
                }
               
            }

        }
    }
}
