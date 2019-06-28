using System.Collections.Generic;

namespace Map
{
    public interface ILayer
    {
        IList<IGeom> Geoms { get; set; }
    }

}
