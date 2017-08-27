using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavascriptDebugger
{
    public class MegaPrint
    {
        private static String print="";

        public static void WriteToPrint(String add)
        {
            print += add;
        }

        public static void WriteLineToPrint(String add)
        {
            print += add + Environment.NewLine;
        }
        public static void NewLine()
        {
            print += Environment.NewLine;
        }



        public static void Flush()
        {
            Console.WriteLine(print);
            print = "";
        }



    }
}
