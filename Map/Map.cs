using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public class Map : Control, IMap
    {
        public IList<ILayer> Layers { get; set; }

        public IMapFrame MapFrame { get; set; }

        double SensivityWheel = 0.1;

        private ISize difference = new MySize(0, 0);

        private SPoint oldPoint ;
        
        public Map(Size size)
       {
            Size = size;
            Layers = new List<ILayer>();
            MapFrame = new MapFrame(Util.Convert(size));
            ListenEvents();
       }

        public void ListenEvents()
        {
            Paint += Map_Paint;
            MouseWheel += Map_MouseWheel;
            MouseMove += Map_MouseMove;
            SizeChanged += Map_SizeChanged;

        }

        private void Map_SizeChanged(object sender, EventArgs e)
        {
            MapFrame.ConstSize = Util.Convert(Size);
            MapFrame.Scale = MapFrame.Scale;
        }

        public void ToCenter()
        {
            MapFrame.Start = new Coor(-MapFrame.Size.Width / 2, -MapFrame.Size.Height / 2);
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (oldPoint == null)
            {
                oldPoint = e.Location;
            }
            else
            {
                difference.Width = -(e.Location.X - oldPoint.X);
                difference.Height = -(e.Location.Y - oldPoint.Y);

                if (e.Button == MouseButtons.Left)
                {
                    Move(difference);
                    Refresh();
                }
            }

            oldPoint = e.Location;
        }

        private void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            double deltaScale = MapFrame.Scale;
            if (e.Delta > 0)
            {
                deltaScale += 0.01;
            }
            else
            {
                deltaScale -= 0.01;
            }

            Zoom(MapFrame.Center, deltaScale);
            Refresh();
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            var t1 = DateTime.Now;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            double calcTime = 0;
            double paintTime = 0;
            foreach (var layer in Layers)
            {
                foreach (var geom in layer.Geoms)
                {
                    var tt1 = DateTime.Now;
                    var list = GetPoints(geom);
                    var tt2 = DateTime.Now;
                    geom.Painter.Paint(e, list);
                    var tt3 = DateTime.Now;

                    calcTime += (tt2 - tt1).Ticks;
                    paintTime += (tt3 - tt2).Ticks;
                }
            }

            //DrawStr(e, MapFrame.Scale.ToString(), 5, 5);
            //DrawStr(e, $"{((int)MapFrame.Size.Width).ToString() } , {((int)MapFrame.Size.Height).ToString() }"  , 5, 20);
            //DrawStr(e, $"{((int)MapFrame.Start.X).ToString() } , {((int)MapFrame.Start.Y).ToString() }"  , 5, 35);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 100, 100, 100)), new Rectangle(0, 0, 500, 35));

            var t2 = DateTime.Now;

            var delta = t2 - t1;
            int perPaint = (int) ((100 * paintTime) / delta.Ticks);
            int perCalc = (int)((100 * calcTime) / delta.Ticks);

            DrawStr(e, $" Paint : {(int)paintTime} ({perPaint}%) , Calc : {(int)calcTime} ({perCalc}%) , Total : {(int)delta.Ticks}", 5, 5);
            DrawStr(e, "FPS : " + ((int)(1000 / delta.TotalMilliseconds)).ToString(), 5, 20);
        }

        private void DrawStr(PaintEventArgs e, String str, int x, int y)
        {
            e.Graphics.DrawString(str, DefaultFont, Brushes.Black, new SPoint(x, y));
        }

        public IList<SPoint> GetPoints(IGeom geom)
        {
            return GetPoints(geom.Coors);
        }

        public IList<SPoint> GetPoints(IList<ICoor> coors)
        {
            var points = new List<SPoint>();

            foreach (var coor in coors)
            {
                points.Add(MapToScreen(coor));
            }

            return points;
        }

        public SPoint MapToScreen(ICoor coor)
        {
            double realX = (coor.X - MapFrame.Start.X) * MapFrame.Scale;
            double realY = (coor.Y - MapFrame.Start.Y) * MapFrame.Scale;

            return new SPoint((int) realX,(int)realY);
        }
         
        /*public ICoor ScreenToMap(SPoint point)
        {
            
        }*/

        public override void Refresh()
        {
            Invalidate();
        }

        public void Zoom(ICoor coor, double scale)
        {
            if(scale < 0)
            {
                scale = 0.05;
            }
            ISize oldSize = (ISize) MapFrame.Size.Clone();
            MapFrame.Scale = scale;
            var Width  = (MapFrame.Size.Width  - oldSize.Width ) / 2;
            var Height = (MapFrame.Size.Height - oldSize.Height) / 2;
            MapFrame.Start.X -= Width;
            MapFrame.Start.Y -= Height;
        }

        public void Move(ISize size)
        {
            MapFrame.Start.X += size.Width * 1/MapFrame.Scale;
            MapFrame.Start.Y += size.Height * 1/MapFrame.Scale;
        }

        public void Goto(ICoor coor)
        {
            MapFrame.Start = new Coor(coor.X, coor.Y);
            Refresh();
        }
    }
}
