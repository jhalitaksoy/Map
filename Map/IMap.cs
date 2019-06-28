using System.Collections.Generic;

namespace Map
{
    public interface IMap
    {
        IList<ILayer> Layers { get; set; }

        IMapFrame MapFrame { get; set; }

        void ToCenter();

        void Refresh();

        void Zoom(ICoor coor, double scale);

        void Move(ISize size);

        void Goto(ICoor coor);
    }
}
