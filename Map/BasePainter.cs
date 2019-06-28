using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public abstract class BasePainter : IPainter
    {
        public Pen Pen { get; set; }

        public Brush Brush { get; set; }

        public BasePainter()
        {
            Pen = Pens.Black;
            Brush = Brushes.White;
        }

        public abstract void Paint(PaintEventArgs e, IList<SPoint> points);
    }

}
