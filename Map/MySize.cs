namespace Map
{
    public class MySize : ISize
    {
        public double Width { get ; set ; }
        public double Height { get ; set ; }

        public MySize()
        {

        }

        public MySize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void Multiply(double value)
        {
            Width *= value;
            Height *= value;
        }

        public object Clone()
        {
            return new MySize(Width, Height);
        }
    }

}
