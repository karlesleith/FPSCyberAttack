using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l337_Server
{
    class Database
    {

        //Adding a new Account to the Database based on UserName and Password
        public void AddAcc(String un, String pw)
        {

            var DB_RS = Globals.mysql.DB_RS;
            DB_RS.Open("SELECT * FROM accounts WHERE 0=1", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
            DB_RS.AddNew();
            //Sends the 2 strings to the DataBase

            //Debug
            Console.WriteLine("Username: "+un + " Password: "+pw+" Sent to the FLI Database");

            DB_RS.Fields["Username"].Value = un;
            DB_RS.Fields["Password"].Value = pw;

            //Updates the Table
            DB_RS.Update();
            //Closes the Database
            DB_RS.Close();
        }
    }
}
