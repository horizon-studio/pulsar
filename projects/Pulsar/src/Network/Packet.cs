using System;

namespace Pulsar.Network
{
    public class Packet
    {
        public string Sender;
        public string Timespan;
        public short Id;
        public short StructId;
        public byte[] data;
    }
}