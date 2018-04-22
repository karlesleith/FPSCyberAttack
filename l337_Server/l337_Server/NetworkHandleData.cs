using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l337_Server
{
    class NetworkHandleData
    {
        private delegate void Packet_(int Index, byte[] Data);
        private Dictionary<int, Packet_> Packets;
        

        //Set up the packest that will be passed
        public void SetUpMessages()
        {
            Packets = new Dictionary<int, Packet_>();
            Packets.Add(1, HandleNewAcc);
        }

        //When the data comes in, do some stuff with it
        public void HandleData(int index, byte[] data)
        {
            int packetnum;
            Packet_ Packet;
            FLI.ByteBuffer buffer = new FLI.ByteBuffer();
            buffer.WriteBytes(data);
            packetnum = buffer.ReadInt();
            buffer = null;

            if (packetnum == 0)
                return;

            if (Packets.TryGetValue(packetnum, out Packet))
            {
                Packet.Invoke(index, data);
            }

        }


        public void HandleNewAcc(int index, byte[] data)
        {
            FLI.ByteBuffer buffer = new FLI.ByteBuffer();
            buffer.WriteBytes(data);


            //IMPORTANT ALWAYS TAKE IN DATA THE SAME WAY IT WAS SENT!
            int packectNum = buffer.ReadInt();

            String username = buffer.ReadString();
            String password = buffer.ReadString();

            //Debug Input
            Console.WriteLine("Debug Username: " + username);
            Console.WriteLine("Debug Password: " + password);

            ///
            Globals.database.AddAcc(username,password);

        }
    }

}
