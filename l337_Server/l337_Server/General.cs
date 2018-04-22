using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l337_Server
{
    class General
    {
        public void InitServer()
        {
            Globals.mysql.MySQLInit();
            InitGameData();
            Globals.network.InitTCP();
        }

        public void InitGameData()
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                Globals.Clients[i] = new Client();
            }
        }
    }
}
