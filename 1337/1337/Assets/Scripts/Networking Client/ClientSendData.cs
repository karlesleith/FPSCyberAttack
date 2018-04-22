using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ClientSendData : MonoBehaviour
{
    public static ClientSendData instance;
    public Network network;

    public Text username;
    public Text password;

    public Text userLogin;
    public Text passLogin;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public void SendDataToServer(byte[] data)
    {

        //Using custom FourLeafInteractive library as a buffer
        FLI.ByteBuffer buffer = new FLI.ByteBuffer(); 
        buffer.WriteBytes(data);
        network.myStream.Write(buffer.toArray(), 0, buffer.toArray().Length);
        buffer = null;
    }


    //On Click Send to Server
    //Sends new Account Info to Database
    public void SendNewAcc()
    {
        FLI.ByteBuffer buffer = new FLI.ByteBuffer();
        buffer.WriteInt(1);
        buffer.WriteString(username.text);
        buffer.WriteString(password.text);

        SendDataToServer(buffer.toArray());
        buffer = null;

    }

    //On click send to server 
    //Sending Login Data to the Databse
    public void SendLogin()
    {
        FLI.ByteBuffer buffer = new FLI.ByteBuffer();
        //Changed to second packet
        buffer.WriteInt(2);
        buffer.WriteString(userLogin.text);
        buffer.WriteString(passLogin.text);

        SendDataToServer(buffer.toArray());
        buffer = null;

    }

}
