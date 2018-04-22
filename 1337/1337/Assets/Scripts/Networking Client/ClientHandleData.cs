using System;
using UnityEngine;
using UnityEngine.UI;

public class ClientHandleData : MonoBehaviour
{

    public static ClientHandleData instance;

    private void Awake()
    {
        instance = this;
    }


    
    void HandleMessages(int packetNum, byte[] data)
    {
        switch (packetNum)
        {
            case 1:
                HandleJoinGame(packetNum,data);
                break;
            case 2:
                HandleInstantiatePlayer(packetNum, data);
                break;
            case 3:
                HandleGetOtherPlayer(packetNum, data);
                break;
        }
    }

    public void HandleData(byte[] data)
    {
        int packetnum;
        FLI.ByteBuffer buffer= new FLI.ByteBuffer();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInt();
        buffer = null;
        if (packetnum == 0)
            return;

        HandleMessages(packetnum, data);
    }

    void HandleJoinGame(int packetNum,byte[]data)
    {
        int packetnum;
        FLI.ByteBuffer buffer = new FLI.ByteBuffer();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInt();
        int MyIndex = buffer.ReadInt();

        Console.WriteLine(MyIndex);
        Globals.instance.MyIndex = MyIndex;
        Network.instance.playerPref = Instantiate(Network.instance.playerPref, Network.instance.spawnPoint);
        Network.instance.playerPref.name = "Player: " + MyIndex.ToString();
        Network.instance.playerPref.GetComponent<NetPlayer>().Index = MyIndex;
    }

    //Player Setup
    public void HandleInstantiatePlayer(int packetNum,byte[]data)
    {
        int packetnum;
        FLI.ByteBuffer buffer = new FLI.ByteBuffer();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInt();

        int PlayerIndex = buffer.ReadInt();
        Network.instance.playerPref = Instantiate(Network.instance.playerPref, Network.instance.spawnPoint);
        Network.instance.playerPref.name = "Player: " + PlayerIndex.ToString();
        Network.instance.playerPref.GetComponent<NetPlayer>().Index = PlayerIndex;
    }

    //Handling connection to other players
    public void HandleGetOtherPlayer(int packetNum,byte[]data)
    {
        int packetnum;
        FLI.ByteBuffer buffer = new FLI.ByteBuffer(); ;
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInt();
        int PlayerIndex = buffer.ReadInt();
        Network.instance.playerPref = Instantiate(Network.instance.playerPref, Network.instance.spawnPoint);
        Network.instance.playerPref.name = "Player: " + PlayerIndex.ToString();
        Network.instance.playerPref.GetComponent<NetPlayer>().Index = PlayerIndex;
    }
}
