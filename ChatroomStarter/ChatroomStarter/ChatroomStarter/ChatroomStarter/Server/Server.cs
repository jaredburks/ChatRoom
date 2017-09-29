using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static Client client;
        TcpListener server;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9191);
            server.Start();
        }
        public void Run()
        {
            AcceptClient();
            //Thread acceptUser = new Thread(AcceptClient);
            string message = client.Recieve();
            Respond(message);
        }
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            Process.Start(@"C:\Users\Jared\Documents\Visual Studio 2015\Projects\ChatRoom\ChatroomStarter\ChatroomStarter\ChatroomStarter\ChatroomStarter\Client\bin\Debug\Client.exe");
            clientSocket = server.AcceptTcpClient();
            //Console.WriteLine("Connected");
            NetworkStream stream = clientSocket.GetStream();
            client = new Client(stream, clientSocket);
            Console.WriteLine(client.UserId + " Connected");
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}
