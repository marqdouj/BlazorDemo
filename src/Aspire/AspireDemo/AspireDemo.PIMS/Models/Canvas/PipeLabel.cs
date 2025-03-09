namespace AspireDemo.PIMS.Models.Canvas
{
    internal record PipeLabel(string Background, string Foreground)
    {
        public PipeLabel(ColorName Background, ColorName Foreground)
            : this(Background.ToString(), Foreground.ToString()) { }
    }
}
