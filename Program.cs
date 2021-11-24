using System;

namespace ClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string MyIp = "127.0.0.125";
            int port = 1025;
            string inSelect = ""; 
            MyClientServer myClientServer = new MyClientServer();

            while (inSelect != "x")
            {
                Console.WriteLine("create (S)erver) / (C)lient) (X)eit ?");

                inSelect = Console.ReadLine().ToLower();
                Console.WriteLine(inSelect);
                if (inSelect == "x") break;

               // bool avto = avtoOrHand();
                // MyClientServer myClientServer = new MyClientServer(MyIp, port, avto);

               // myClientServer.settings(MyIp, port, avto);
                myClientServer.settings(setIp(), setPort(), avtoOrHand());


                if (inSelect == "s")
                {
                    myClientServer.StartServer();
                }

                if (inSelect == "c")
                {
                    myClientServer.StartClient();
                }

                string setIp()
                {
                    Console.WriteLine("write IP adress: ");
                    return Console.ReadLine().Trim();
                }
                int setPort()
                {
                    Console.WriteLine("write number port: ");
                    return int.Parse(Console.ReadLine().Trim());
                }
                bool avtoOrHand()
                {
                    Console.WriteLine("Dialog (A)avto (H) hand ?");
                    return Console.ReadLine().ToLower() == "a";
                }
            }
        }
    }
}
