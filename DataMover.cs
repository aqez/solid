using System.IO;
using System.Net;

namespace SOLID
{
    public interface IDataProvider
    {
        byte[] GetData();
    }

    public class FileDataProvider : IDataProvider
    {
        private readonly string _fileName;

        public FileDataProvider(string fileName)
        {
            _fileName = fileName;
        }

        public byte[] GetData()
        {
            using (FileStream fs = new FileStream(_fileName, FileMode.Open))
            using (MemoryStream ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    public class WebClientDataProvider : IDataProvider
    {
        private readonly string _url;

        public WebClientDataProvider(string url)
        {
            _url = url;
        }

        public byte[] GetData()
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadData(_url);
            }
        }
    }

    public interface IDataSender
    {
        void SendData(byte[] data);
    }

    public class WebClientDataSender : IDataSender
    {
        private readonly string _ipAddress;

        public WebClientDataSender(string ipAddress)
        {
            _ipAddress = ipAddress;
        }

        public void SendData(byte[] data)
        {
            using (WebClient wc = new WebClient())
            {
                wc.UploadData(_ipAddress, data);
            }
        }
    }


    public class FtpDataSender : IDataSender
    {
        public void SendData(byte[] data)
        {
            // TODO: Implement for FTP
        }
    }

    public class DataMover
    {
        private readonly IDataProvider _dataProvider;
        private readonly IDataSender _dataSender;

        public DataMover(IDataProvider dataProvider, IDataSender dataSender)
        {
            _dataProvider = dataProvider;
            _dataSender = dataSender;
        }

        public void MoveData()
        {
            byte[] bytes = _dataProvider.GetData();
            _dataSender.SendData(bytes);
        }

    }
}
