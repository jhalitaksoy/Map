namespace Map
{
    public class Coor : ICoor
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double M { get; set; }

        public Coor(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
            M = 0;
        }

        public Coor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            M = 0;
        }

        public static readonly Coor Empty = new Coor(0, 0);
    }

}
