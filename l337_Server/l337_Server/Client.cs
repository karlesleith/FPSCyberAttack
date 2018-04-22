using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace l337_Server
{
    //Establish connection to Client
    class Client
    {
            public int Index;
            public string IP;
            public TcpClient Socket;
            public NetworkStream myStream;
            private byte[] readBuff;


            public void Start()
            {
                Socket.SendBufferSize = 4096;
                Socket.ReceiveBufferSize = 4096;
                myStream = Socket.GetStream();
                Array.Resize(ref readBuff, Socket.ReceiveBufferSize);
                myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnDataIn, null);
            }

        //Close the Connection to the Client
            void CloseConnection()
            {
                Socket.Close();
                Socket = null;
            }


            void OnDataIn(IAsyncResult result)
            {
                try
                {
                    int readBytes = myStream.EndRead(result);
                    if (Socket == null)
                    {
                        return;
                    }
                    if (readBytes <= 0)
                    {
                        CloseConnection();
                        return;
                    }

                    byte[] newBytes = null;
                    Array.Resize(ref newBytes, readBytes);
                    Buffer.BlockCopy(readBuff, 0, newBytes, 0, readBytes);

                    Globals.networkHandleData.HandleData(Index, newBytes);

                    if (Socket == null)
                    {
                        return;
                    }

                    myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnDataIn, null);
                }
                catch (Exception ex)
                {
                    CloseConnection();
                    return;

                }
            }
        }
    }

