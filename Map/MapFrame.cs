namespace Map
{
    public class MapFrame : IMapFrame
    {
        public ICoor Start { get ; set ; }

        public ISize Size { get; set ; }

        public ISize ConstSize { get; set; }

        private double scale = 1;

        public double Scale {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                Size = (ISize)ConstSize.Clone();
                Size.Multiply(1/scale);
            }
        }
        public ICoor Center { get; set; }

        public MapFrame(ISize size)
        {
            Start = Coor.Empty;
            Size = size;
            ConstSize = (ISize)Size.Clone();
            Scale = 1;
        }
    }

}
