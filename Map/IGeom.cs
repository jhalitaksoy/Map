using System.Collections.Generic;

namespace Map
{
    public interface IGeom
    {
        GeoType Type { get;}

        IList<ICoor> Coors { get; set; }

        IPainter Painter { get; set; }
    }

}
