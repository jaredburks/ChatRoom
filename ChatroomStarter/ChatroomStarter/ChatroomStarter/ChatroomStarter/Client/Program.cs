using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1", 9191);
            Thread send = new Thread(client.KeepSending);
            Thread receive = new Thread(client.KeepReceiving);
            send.Start();
            receive.Start();
            //client.Send();
            //client.Recieve();
            Console.ReadLine();
        }
    }
}
