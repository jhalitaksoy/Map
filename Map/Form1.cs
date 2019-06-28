using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPoint = System.Drawing.Point;

namespace Map
{
    public partial class Form1 : Form
    {
        public IMap Map;

        public Form1()
        {
            InitializeComponent();

            Map = AddMap();
            AddLayers(Map);
            Map.ToCenter();
            Map.Refresh();
        }

        public void AddLayers(IMap map)
        {
            var layer = new Layer();
            var line = new MLine();
            line.Coors.Add(new Coor(0, 0));
            line.Coors.Add(new Coor(100, 100));
            layer.Geoms.Add(line);
            map.Layers.Add(layer);
        }

        public IMap AddMap()
        {
            Map map = new Map(Size - new Size(40, 60));
            map.BackColor = Color.White;
            Size = new Size(800, 500);
            map.Location = new SPoint(10, 10);
            //map.Size = Size - new Size(40, 60);
            map.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Controls.Add(map);
            return map;
        }
    }
}
