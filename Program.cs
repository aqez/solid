using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace SOLID
{
    class Program
    {

        static void Main(string[] args)
        {

            IEnumerable<IRectangle> rectangles = new RectangleProvider().GetRectangles();

            foreach (IRectangle rectangle in rectangles)
            {
                int startinArea = rectangle.Area;

                int startingWidth = rectangle.Width;
                int startingHeight = rectangle.Height;

                rectangle.Width += 1;

                if (rectangle.Area != startinArea + startingHeight)
                {
                    throw new ApplicationException("Failed somehow");
                }
            }

            // Now that we made DataMover follow the SOLID principles,
            // we can move data in all sorts of way without ever actually
            // touching the logic inside DataMover itself - just by adding
            // new implementations of the IDataProvider and IDataSender
            // interfaces! Go check out that file (DataMover.cs) for more info!
            DataMover fileToIpMover = new DataMover(
                    new FileDataProvider("someFile.txt"),
                    new WebClientDataSender("192.168.0.10"));


            DataMover webToFtpMover = new DataMover(
                    new WebClientDataProvider("google.com"),
                    new FtpDataSender());


            Console.WriteLine("Hello World!");
        }
    }
}
