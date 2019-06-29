using netDxf;
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
            OpenDxfFile("E:\\Arazi_Toplulastirma\\DataSet\\çaltı_kadastro_son.dxf");
            Map.Goto(Map.Layers[0].Geoms[0].Coors[0]);
            //Map.ToCenter();
            //Map.Refresh();

        }

        public void OpenFile()
        {
            var fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenDxfFile(fileDialog.FileName);
            }
        }

        public void OpenDxfFile(string path)
        {
            try
            {
                var layer = new Layer();
                var dxfDoc = DxfDocument.Load(path);

                foreach (var poly in dxfDoc.Polylines)
                {
                    if(poly.Layer.Name != "KADASTRO")
                    {
                        continue;
                    }
                    var polygon = new Polygon();
                    foreach (var vertex in poly.Vertexes)
                    {
                        polygon.Coors.Add(new Coor(vertex.Position.X, vertex.Position.Y));
                    }
                    layer.Geoms.Add(polygon);
                }
                Map.Layers.Add(layer);
            }
            catch (Exception)
            {

            }
            
        }

        public IMap AddMap()
        {
            Map map = new Map(Size - new Size(40, 60));
            
            map.Size = new System.Drawing.Size(768, 443);
            //map.BackColor = Color.White;
            Size = new Size(800, 500);
            map.Location = new SPoint(10, 10);
            //map.Size = Size - new Size(40, 60);
            map.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Controls.Add(map);
            return map;
        }
    }
}
