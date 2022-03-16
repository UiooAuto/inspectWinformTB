using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace inspectWinformTB
{
    public class SocketTool
    {
        private Socket socket;
        public IPAddress ipAddress;
        public int serverPort;
        public IPEndPoint ipEndPoint;
        public int overTime = 1000;
        public Ping ping;
    }
}
