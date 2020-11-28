using System.Collections.Generic;

namespace SOLID
{
    public class RectangleProvider
    {
        public IEnumerable<IRectangle> GetRectangles()
        {
            yield return new Rectangle { Width = 30, Height = 20 }
            yield return new Rectangle { Width = 55, Height = 28 }

            yield return new Square { Width = 22 }
        }
    }
}
