using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public class Polygon
    {
        public GeoType Type {
            get
            {
                return GeoType.Polygon;
            }
        }
        public IList<ICoor> Coors { get ; set; }

        public IPainter Painter { get; set; }

        public Polygon()
        {
            Coors = new List<ICoor>();
            Painter = new PolyPainter();
        }

        public class PolyPainter : BasePainter
        {
            public override void Paint(PaintEventArgs e, IList<SPoint> points)
            {
                e.Graphics.DrawPolygon(Pen, points.ToArray());
            }
        }
    }

}
