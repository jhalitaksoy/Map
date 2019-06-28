namespace Map
{
    public interface IMapFrame
    {
        ICoor Start { get; set; }

        ISize Size { get; set; }

        ICoor Center { get; set; }

        double Scale { get; set; }

        ISize ConstSize { get; set; }
    }

}
