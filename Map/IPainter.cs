using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public interface IPainter
    {
        Pen Pen { get; set; }

        Brush Brush { get; set; }

        void Paint(PaintEventArgs e, IList<SPoint> points);
    }

}
