using System;
using System.Net.Sockets;
using System.Text;

namespace KBZ.Utils.Infrastructure
{
    public class StateObject
    {
        public StateObject()
        {

        }
        public static String content = String.Empty;
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
}
