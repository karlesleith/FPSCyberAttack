using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l337_Server
{
    class Globals { 
         public static Globals instance = new Globals();

    //Global instances of classes.
    //This is Data that is share between all players

    public static General general = new General();
    public static Network network = new Network();
    public static Database database = new Database();
    public static MySQL mysql = new MySQL();
    public static NetworkHandleData networkHandleData = new NetworkHandleData();
    public static NetworkSendData networkSendData = new NetworkSendData();
    public static Client[] Clients = new Client[Constants.MAX_PLAYERS];

    //
    public int Player_HighIndex;
    
    }
}
