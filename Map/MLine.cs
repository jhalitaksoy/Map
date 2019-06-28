using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public class MLine : IGeom
    {
        public GeoType Type
        {
            get
            {
                return GeoType.Mline;
            }
        }
        public IList<ICoor> Coors { get; set; }
        public IPainter Painter { get ; set; }

        public MLine()
        {
            Coors = new List<ICoor>();
            Painter = new MLinePainter();
        }

        public class MLinePainter : BasePainter
        {
            public override void Paint(PaintEventArgs e, IList<SPoint> points)
            {
                e.Graphics.DrawLines(Pen, points.ToArray());
            }
        }
    }

}
