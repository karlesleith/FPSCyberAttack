using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADODB;
namespace l337_Server
{
    class MySQL
    {
        //Getting the database connectuon
        public Recordset DB_RS;
        public Connection DB_CONN;


        public void MySQLInit()
        {

            try
            {
                //Try catch to establish connection
                DB_RS = new Recordset();
                DB_CONN = new Connection();

                //Connected to the "gamedatabase for 1337"
                DB_CONN.ConnectionString = "Driver={MySQL ODBC 3.51 Driver};Server=localhost;Port=3306;Database=gamedatabase;User=root;Password=;Option=3;";
                DB_CONN.CursorLocation = CursorLocationEnum.adUseServer;
                DB_CONN.Open();
                Console.WriteLine("Connection to 1337 Server Has been Established");

                var db = DB_RS;
                {
                  //   db.Open("SELECT * FROM accounts WHERE 0=1", DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                    //db.AddNew();
                    //db.Fields["username"].Value = "Unity";
                    //db.Fields["password"].Value = "Unity";
                    //db.Update();
                    //db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

    }
}
