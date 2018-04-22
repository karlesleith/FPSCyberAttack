using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l337_Server
{
    class NetworkSendData
    {
        //Send data to client
        public void SendDataTo(int index, byte[] data)
        {
            FLI.ByteBuffer buffer = new FLI.ByteBuffer();
            buffer.WriteBytes(data);
            Globals.Clients[index].myStream.BeginWrite(buffer.toArray(), 0, buffer.toArray().Length, null, null);
            buffer = null;
        }


        public void SendAlertMessage(int indext, string mes)
        {
            FLI.ByteBuffer buffer = new FLI.ByteBuffer();
            //Set our package to 1
            buffer.WriteInt(1);
            buffer.WriteString(mes);

            SendDataTo(indext, buffer.toArray());

        }

        //Send data to all clients
        public async void SendDataToAll(byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket != null)
                {
                    await Task.Delay(1000);
                    SendDataTo(i, data);
                }
            }
        }
        public void SendDataToAllBut(int index, byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket != null)
                {
                    if (i != index)
                    {
                        SendDataTo(i, data);
                    }
                }
            }
        }


        }
    }


