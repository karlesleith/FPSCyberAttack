using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace l337_Server
{
    //Class to Start the Server
    class Server
    {
    
        private static Thread threadConsole;
        private static bool consoleRunning;


        //Start up Console
        static void Main(string[] args)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();

            Globals.networkHandleData.SetUpMessages();
            Globals.general.InitServer();
        }

        private static void ConsoleThread()
        {
            string line;
            consoleRunning = true;

            while (consoleRunning)
            {
                line = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(line))
                {
                    consoleRunning = false;
                    return;
                }

                else
                {

                }
            }
        }
    }
}

