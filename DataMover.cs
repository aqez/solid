using System.IO;
using System.Net;

namespace SOLID
{
    public class DataMover
    {
        private byte[] _data;

        public void GetData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            using (MemoryStream ms = new MemoryStream(_data))
            {
                fs.CopyTo(ms);
            }
        }

        public void SendData(string ipAddress)
        {
            using (WebClient wc = new WebClient())
            {
                wc.UploadData(ipAddress, _data);
            }
        }
    }
}
