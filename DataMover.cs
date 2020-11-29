using System.IO;
using System.Net;

namespace SOLID
{
    /// <summary>
    /// Because we pulled this interface out of DataProvider, which itself came out of
    /// DataMover, we can now implement many ways of 'getting data' in order to move
    /// that data using the DataMover class.
    /// </summary>
    public interface IDataProvider
    {
        byte[] GetData();
    }

    /// <summary>
    /// This is one example of a data provider. It uses a constructor to take in its
    /// 'dependencies' (in this case a file name). Other impelmentations of the
    /// IDataProvider interface do not necessarily need a fileName, so it does not
    /// make sense to include that anywhere but here. That is part of keeping your
    /// interfaces segregated.
    /// </summary>
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

    /// <summary>
    /// Another implementation of IDataProvider, this one using a string url
    /// instead of a file name to get its data. Ideally this class itself would
    /// use an interface in order to obtain data from the url, instead of depending
    /// directly on WebClient. That way it could be tested easily by mocking out
    /// whatever method is needed to download the data.
    /// </summary>
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

    /// <summary>
    /// This interface was pulled out of WebClientDataSender, and before that
    /// out of DataMover. Now that we have this interface we can also implement
    /// other methos of moving data around.
    /// </summary>
    public interface IDataSender
    {
        void SendData(byte[] data);
    }

    /// <summary>
    /// This was the original impelmentation of IDataSender, and it was pulled
    /// out of DataMover in order to separate responsibilities. If WebClientDataSender
    /// and FileDataProviders' logic were still both in DataMover, then any time
    /// you had to change how moving data worked you could mess up other parts
    /// of the class that have nothing to do with each  other. This is the clear
    /// sign that you should introduce indirection by moving the logic out into classes
    /// like we did in the video.
    /// </summary>
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

    /// <summary>
    /// This class originally had way too much responsibility. It was in charge
    /// of getting bytes out of a file as well as sending them to an ip address
    /// over the network. We also didn't mention it in the video but the class
    /// originally also had dependencies on 'fileName' and 'ipAddress' which
    /// made it very hard to change its behavior without possibly introducing
    /// bugs in existing code! Now that this logic has all been called indirectly
    /// through interfaces, we can move data around using any kinds of methods
    /// and the over all logic will stay the same!
    /// </summary>
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
