using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JavascriptDebugger.Inputhandler
{
    public class GlobalUpdate : InputType
    {
        public GlobalUpdate()
        {

        }
        public override void Execute(String data,String time)
        {
            TrackList TL = new TrackList(data);
            String space = "    ";
            int i = 0;
            MegaPrint.NewLine();
            int width = Console.WindowWidth;
            String Line = String.Concat(Enumerable.Repeat("-", width - 2));
            Line = "#" + Line + "#";
            MegaPrint.WriteLineToPrint(Line);
            MegaPrint.NewLine();

            MegaPrint.WriteLineToPrint(space + space + "Name" + space + "value" + space + "Global");
            foreach(DebugObj d in TL.list)
            {
                MegaPrint.WriteLineToPrint(space + i + space + d.name + space +d.value + space + d.isGlobal);
                i++;
            }
            MegaPrint.NewLine();
            MegaPrint.WriteLineToPrint(Line);
            Console.Clear();
            MegaPrint.Flush();
            //Console.WriteLine(data);
        }

    }
}
