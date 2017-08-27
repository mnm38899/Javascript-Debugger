using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JavascriptDebugger
{
    public static class PacketHandler
    {
        public static void Handle(Byte[] packet, Socket clientSocket)
        {
            //Console.WriteLine();
            String data = BitConverter.ToString(packet,0,packet.Length);
            Console.WriteLine(data);
        }
    }
}
