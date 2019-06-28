using System.Collections.Generic;

namespace Map
{
    public class Layer : ILayer
    {
        public IList<IGeom> Geoms { get; set; }

        public Layer()
        {
            Geoms = new List<IGeom>();
        }
    }

}
