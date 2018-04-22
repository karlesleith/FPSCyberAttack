using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Network : MonoBehaviour
{ 
    //Networking on client create a new instance as every player is independent
    public static Network instance;

    public GameObject playerPref;
    public Transform spawnPoint;

    public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    public bool isConnected;

    public TcpClient PlayerSocket;
    public NetworkStream myStream;
    public StreamReader myReader;
    public StreamWriter myWriter;

    private byte[] asyncBuff;
    public bool shouldHandleData;
    private byte[] myBytes;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        ConnectGameServer();
    }

    void ConnectGameServer()
    {
        if (PlayerSocket != null)
        {
            if (PlayerSocket.Connected || isConnected)
                return;
            PlayerSocket.Close();
            PlayerSocket = null;
        }

        PlayerSocket = new TcpClient();
        PlayerSocket.ReceiveBufferSize = 4096;
        PlayerSocket.SendBufferSize = 4096;
        PlayerSocket.NoDelay = false;
        Array.Resize(ref asyncBuff, 8192);
        PlayerSocket.BeginConnect(ServerIP, ServerPort, new AsyncCallback(ConnectCallback), PlayerSocket);
        isConnected = true;
    }

    void ConnectCallback(IAsyncResult result)
    {
        if (PlayerSocket != null)
        {
            PlayerSocket.EndConnect(result);
            if (PlayerSocket.Connected == false)
            {
                isConnected = false;
                return;
            }
            else
            {
                PlayerSocket.NoDelay = true;
                myStream = PlayerSocket.GetStream();
                myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
            }
        }
    }

    private void Update()
    {
        if (shouldHandleData)
        {
            ClientHandleData.instance.HandleData(myBytes);
            shouldHandleData = false;
        }
    }

    void OnReceive(IAsyncResult result)
    {
        if (PlayerSocket != null)
        {
            if (PlayerSocket == null)
                return;

            int byteArray = myStream.EndRead(result);
            myBytes = null;
            Array.Resize(ref myBytes, byteArray);
            Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteArray);

            if (byteArray == 0)
            {
                Debug.Log("You Have now been Disconnected from the FLI Server...");
                PlayerSocket.Close();
                return;
            }

            shouldHandleData = true;

            if (PlayerSocket == null)
                return;
            myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
        }
    }
}
