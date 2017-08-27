using JavascriptDebugger.InputHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace JavascriptDebugger
{
    public class DebugServer
    {
        private Socket _socket;
        private Byte[] _buffer = new Byte[32768];
        private int MessageCounter = 0;
        public Boolean isPaused = false;
        public MessageHandler handleMessages;

        public DebugServer()
        {
            
        }

        public void Start()
        {
            Console.WriteLine("Starting server...");
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _socket.Bind(new IPEndPoint(IPAddress.Any, 30001));
            _socket.Listen(5);
            _socket.BeginAccept(null, 0, new AsyncCallback(OnAccept), null);
            Console.WriteLine("Server is running on port 30001");
        }

        private void OnAccept(IAsyncResult result)
        {
            try
            {
                Socket client = null;
                if (_socket != null && _socket.IsBound)
                {
                    client = _socket.EndAccept(result);
                }
                if (client != null)
                {
                    /* Handshaking and managing ClientSocket */
                    Console.WriteLine("Client connected!");
                    client.BeginReceive(_buffer, 0, _buffer.Length,SocketFlags.None, new AsyncCallback(recievedCallback), client);
                }
            }
            catch (SocketException exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
            finally
            {
                if (_socket != null && _socket.IsBound)
                {
                    _socket.BeginAccept(null, 0, OnAccept, null);
                }
            }
        }

        private void recievedCallback(IAsyncResult result)
        {
            Socket clientSocket = result.AsyncState as Socket;
            bool isConnected = IsConnected(clientSocket);
            if(isConnected==false)
            {
                return;
            }

            int buffersize = clientSocket.EndReceive(result);
            Byte[] packet = new Byte[buffersize];
            Array.Copy(_buffer,packet,packet.Length);

            String message = Encoding.ASCII.GetString(packet);
            //Console.WriteLine(message);

            if (new Regex("^GET").IsMatch(message))
            {
                //Console.WriteLine("Client: " + message);
                Console.WriteLine("Sending Handshake...");
                Byte[] HandShake = getHandShakeBytes(message);
                clientSocket.Send(HandShake,HandShake.Length,SocketFlags.None);
            }
            else
            {
                String JSON = GetDecodedData(packet,packet.Length);
                
                if(isPaused==false)
                {
                    handleMessages.HandleMessage(JSON);
                }
                
            }
            clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(recievedCallback), clientSocket);
        }

        private bool IsConnected(Socket socket)
        {
            try
            {
                if ((socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0))
                {
                    socket.Close();
                    Console.WriteLine("client disconnected");
                    return false;
                };
            }
            catch (SocketException e)
            {
                socket.Close();
                Console.WriteLine("client disconnected");
                return false;
            }
            return true;
        }



        private Byte[] getHandShakeBytes(String message)
        {
            String response = "HTTP/1.1 101 Switching Protocols" + Environment.NewLine + "Connection: Upgrade" + Environment.NewLine + "Upgrade: websocket" + Environment.NewLine + "Sec-WebSocket-Accept: ";
            String hash = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(new Regex("Sec-WebSocket-Key: (.*)").Match(message).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")));
            response += hash +" "+ Environment.NewLine + Environment.NewLine;
            //Console.WriteLine(response);
            //Encoding.UTF8
            return Encoding.UTF8.GetBytes(response);
        }

        private string GetDecodedData(byte[] buffer, int length)
        {
            if(buffer.Length<=0)
            {
                return "";
            }
            byte b = buffer[1];
            int dataLength = 0;
            int totalLength = 0;
            int keyIndex = 0;

            if (b - 128 <= 125)
            {
                dataLength = b - 128;
                keyIndex = 2;
                totalLength = dataLength + 6;
            }

            if (b - 128 == 126)
            {
                dataLength = BitConverter.ToInt16(new byte[] { buffer[3], buffer[2] }, 0);
                keyIndex = 4;
                totalLength = dataLength + 8;
            }

            if (b - 128 == 127)
            {
                dataLength = (int)BitConverter.ToInt64(new byte[] { buffer[9], buffer[8], buffer[7], buffer[6], buffer[5], buffer[4], buffer[3], buffer[2] }, 0);
                keyIndex = 10;
                totalLength = dataLength + 14;
            }

            if (totalLength > length)
                return "{}";
                //throw new Exception("The buffer length is small than the data length");

            byte[] key = new byte[] { buffer[keyIndex], buffer[keyIndex + 1], buffer[keyIndex + 2], buffer[keyIndex + 3] };

            int dataIndex = keyIndex + 4;
            int count = 0;
            for (int i = dataIndex; i < totalLength; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ key[count % 4]);
                count++;
            }
            dataLength = Math.Abs(dataLength);
            return Encoding.UTF8.GetString(buffer, dataIndex, dataLength);
        }
    }
}
