using System.Drawing;

namespace Map
{
    public class Util
    {
        public static ISize Convert(Size size)
        {
            return new MySize(size.Width, size.Height);
        }
    }

}
