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


            Console.WriteLine("Hello World!");
        }
    }
}
