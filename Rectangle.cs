namespace SOLID
{
    public interface IRectangle
    {
        int Width { get; set; }
        int Height { get; set; }
    }

    public class Rectangle : IRectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }


    public class Square : IRectangle
    {
        private int _sideLength;
        public int Width
        {
            get { return _sideLength; }
            set { _sideLength = value; }
        }

        public int Height
        {
            get { return _sideLength; }
            set { _sideLength = value; }
        }
    }
}
