using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public class Point : IGeom
    {
        public GeoType Type
        {
            get
            {
                return GeoType.Point;
            }
        }
        public IList<ICoor> Coors { get; set; }

        public ICoor Coor
        {
            get
            {
                return Coors[0];
            }

            set
            {
                Coors[0] = value;
            }
        }

        public IPainter Painter { get ; set; }

        public Point(ICoor coor)
        {
            Coors = new List<ICoor> { coor };
            Painter = new PointPainter();
        }

        public class PointPainter : BasePainter
        {
            public override void Paint(PaintEventArgs e, IList<SPoint> points)
            {
                int x = points[0].X;
                int y = points[0].Y;
                e.Graphics.FillRectangle(Brush, new RectangleF(x - 10, y - 10, x + 10, y + 10));
            }
        }
    }

}
