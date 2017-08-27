using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JavascriptDebugger.InputHandler;

namespace JavascriptDebugger
{
    class Program
    {
        public static DebugServer DS;
        public static MessageHandler MH = new MessageHandler();
        static void Main(string[] args)
        {
            Console.Title = "Javascript Variable Tracker - DebugMode";

            //System.Threading.Thread.Sleep(250);


            DS = new DebugServer();
            DS.handleMessages = MH;
            DS.Start();

            Console.CancelKeyPress += ToggleMode;
            //Console.
           
            while(true)
            {
                 Console.ReadLine();
            }
        }

        private static void ToggleMode(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            //Console.WriteLine("Switching mode");
            DS.isPaused = !DS.isPaused;
            if(DS.isPaused==true)
            {
                Console.Title = "Javascript Variable Tracker - InputMode";
            }
            else
            {
                Console.Title = "Javascript Variable Tracker - DebugMode";
            }
        }
    }
}
