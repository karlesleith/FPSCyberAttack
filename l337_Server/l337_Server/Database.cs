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


        //Checking to see if the Username is Already in the database
        public bool AccExist(int index,String un)
        {
            Console.WriteLine("Searching for " + un);

            var DB_RS = Globals.mysql.DB_RS;
            DB_RS.Open("SELECT * FROM accounts WHERE username = '"+un+"'", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);


            //If End of File and Hasnt been found Return False
            if (DB_RS.EOF)
            {

                Globals.networkSendData.SendAlertMessage(index,"Account does not exist");
                Console.WriteLine("No such username");
                DB_RS.Close();
                return false;
            }
            else
            {
                Globals.networkSendData.SendAlertMessage(index, un+" has been Found!");
                Console.WriteLine(un+ " has been Found!");
                DB_RS.Close();
                return true;
            }
            //Closes the Database
            //Inaccessable
             DB_RS.Close();
        }

        //Checking to see if the password is Already in the database
        public bool PasswordMatch(int index, String un, String pw)
        {
            //Console.WriteLine("Searching for " + pw);

            var DB_RS = Globals.mysql.DB_RS;
            DB_RS.Open("SELECT '"+un+"' FROM accounts WHERE password = '" + pw + "'", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);


            //If End of File and Hasnt been found Return False
            if (DB_RS.EOF)
            {

                Globals.networkSendData.SendAlertMessage(index,"Password is incorrect");
                Console.WriteLine("Password is incorrect");
                DB_RS.Close();
                return false;
            }
            else
            {
                Globals.networkSendData.SendAlertMessage(index, "Access Granted!");
                Console.WriteLine("Access Granted!\nHello "+un+ "!");
                DB_RS.Close();
                return true;
            }
            //Closes the Database
            //Inaccessable
            DB_RS.Close();
        }

    }
}
