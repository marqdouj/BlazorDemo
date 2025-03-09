namespace AspireDemo.PIMS.Models.Canvas
{
    public record ColorStop(double Offset, string Color)
    {
        internal ColorStop(double Offset, ColorName Color) : this(Offset, Color.ToString()) { }
    }
}
