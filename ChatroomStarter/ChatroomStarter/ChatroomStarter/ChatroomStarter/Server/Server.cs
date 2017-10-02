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
        Queue<Message> messages;
        Dictionary<string, Client> member;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9191);
            server.Start();
            member = new Dictionary<string, Client>();
            messages = new Queue<Message>();
        }
        public void Run()
        {
            //AcceptClient();
            Thread acceptClient = new Thread(KeepAcceptingClients);
            acceptClient.Start();

            //string message = client.Recieve();
            //Respond(message);
        }
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();
            //Console.WriteLine("Connected");
            NetworkStream stream = clientSocket.GetStream();
            client = new Client(stream, clientSocket);
            Console.WriteLine("Client " + " Connected");
            //member.Add(client.UserId, client);
            Thread receive = new Thread(client.KeepReceiving);
        }
        public void KeepAcceptingClients()
        {
            while (true)
            {
               AcceptClient();
            }
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
        public void QueueMessages()
        {

        }
        //Below is how to open a new Client window in code.
        //Process.Start(@"C:\Users\Jared\Documents\Visual Studio 2015\Projects\ChatRoom\ChatroomStarter\ChatroomStarter\ChatroomStarter\ChatroomStarter\Client\bin\Debug\Client.exe");
    }
}
