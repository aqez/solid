namespace SOLID
{
    public interface IRectangle
    {
        int Width { get; set; }
        int Height { get; set; }

        int Area => Width * Height;
    }

    public class Rectangle : IRectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }


    /// <summary>
    /// This class violates the Liskov Substitution Principle because when acting
    /// upon an IRectangle, the user does not necessarily expect the width and height
    /// to both change upon only changing one of them.
    /// </summary>
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
