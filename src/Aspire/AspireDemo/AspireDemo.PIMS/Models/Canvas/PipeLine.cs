namespace AspireDemo.PIMS.Models.Canvas
{
    internal class PipeLine(ColorName lineStroke = ColorName.Black, LineCap lineCap = LineCap.Butt)
    {
        public List<int> LineDash { get; set; } = [];
        public double LineWidth { get; set; } = 1;
        public string LineStroke { get; set; } = lineStroke.ToString();
        public string LineCap { get; set; } = lineCap.ToString().ToLower();
        public bool Visible { get; set; } = true;
    }
}
