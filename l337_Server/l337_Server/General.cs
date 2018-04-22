using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l337_Server
{

    //General Setup
    class General
    {

        public void SetUpServer()
        {
            Globals.mysql.MySQLInit();
            SetUpGameData();
            Globals.network.InitTCP();
            //Testing
            //Globals.database.PasswordMatch(1,"Unity","Unity");
        }

        public void SetUpGameData()
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                Globals.Clients[i] = new Client();
            }
        }

    
    }
}
