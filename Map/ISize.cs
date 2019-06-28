using System;

namespace Map
{
    public interface ISize : ICloneable
    {
        double Width { get; set; }

        double Height { get; set; }

        void Multiply(double value);

    }

}
