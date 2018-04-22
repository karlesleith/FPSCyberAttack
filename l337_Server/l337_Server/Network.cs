using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace l337_Server
{
    class Network
    {
        public TcpListener ServerSocket;

        //Start the Server on The Local Host
        public void InitTCP()
        {
            ServerSocket = new TcpListener(IPAddress.Any, 5500);
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);
            Console.WriteLine("Server is Now Up and Running");
        }

        //When a client tries to connect to the server on the local host, send the console information of the established connection
        void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = ServerSocket.EndAcceptTcpClient(result);
            client.NoDelay = false;
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket == null)
                {
                    Globals.Clients[i].Socket = client;
                    Globals.Clients[i].Index = i;
                    Globals.Clients[i].IP = client.Client.RemoteEndPoint.ToString();
                    Globals.Clients[i].Start();
                    Console.WriteLine("Incoming Connection from " + Globals.Clients[i].IP + "|| Index: " + i);
                   
                  
                    return;
                }
            }
        }
    }
}
