using AspireDemo.ApiService.Client.Models;
using System.Drawing;

namespace AspireDemo.PIMS.Models.Range
{
    public readonly struct PipeRange
    {
        public PipeRange(VPMEvent e, PipeRangeMeasureUnit unit = PipeRangeMeasureUnit.Meter)
            : this(e.DFS + e.Width / 2.0, e.Width, unit)
        {

        }

        public PipeRange(double center, double width, PipeRangeMeasureUnit unit = PipeRangeMeasureUnit.Meter) : this()
        {
            Center = center;
            Width = width;
            Unit = unit;
            X1 = Center - Width / 2.0;
            X2 = Center + Width / 2.0;
        }

        public double Center { get; }
        public PipeRangeMeasureUnit Unit { get; }
        public double Width { get; }
        public double X1 { get; }
        public double X2 { get; }

        public override string ToString()
        {
            return $"Unit:{Unit} X1:{X1:F2} X2:{X1:F2} Center:{Center} Width:{Width}";
        }
    }

    public static class LinearRangeExtensions
    {
        public static PipeRange Scroll(this PipeRange range, PipeRangeDirection direction, double distance)
        {
            switch (direction)
            {
                case PipeRangeDirection.Upstream:
                    return new(range.Center - distance, range.Width, range.Unit);
                case PipeRangeDirection.Downstream:
                default:
                    return new(range.Center + distance, range.Width, range.Unit); ;
            }
        }

        public static PipeRange GoToDFS(this PipeRange range, double distance)
        {
            return new(distance, range.Width, range.Unit);
        }

        public static bool IntersectsWith(this PipeRange source, PipeRange target)
        {
            var rc1 = new RectangleF((float)source.X1, 0, (float)source.Width, 2);
            var rc2 = new RectangleF((float)target.X1, 1, (float)target.Width, 2);

            return rc1.IntersectsWith(rc2);
        }
    }
}
