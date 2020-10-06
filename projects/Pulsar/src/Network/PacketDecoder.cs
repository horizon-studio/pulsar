using System.IO;
using System.Threading.Tasks;

namespace Pulsar.Network
{
    public class PacketDecoder
    {
        public void Decode(byte[] data)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            reader.ReadString();
        }
    }
}