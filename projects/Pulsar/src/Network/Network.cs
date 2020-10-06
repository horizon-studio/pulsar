using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Pulsar.Network
{
    public class Network
    {
        private UdpClient udp;

        public int Port = 0;
        
        public bool IsRunning { get; set; } = false;
        
        public Network(int port)
        {
            udp = new UdpClient();
            Port = port;
        }

        public void Start()
        {
            IsRunning = true;
            IPEndPoint point = new IPEndPoint(IPAddress.Any, Port);
            while (IsRunning)
            {
                byte[] rec = udp.Receive(ref point);
                //TODO: send to packet pipeline in new task
                ThreadPool.QueueUserWorkItem((data) =>
                {
                    PacketDecoder dec = new PacketDecoder();
                    dec.Decode((byte[])data);
                }, rec);
            }
        }
        
        public void Send(byte[] bytes, uint length)
        {
            
        }
        
    }
}