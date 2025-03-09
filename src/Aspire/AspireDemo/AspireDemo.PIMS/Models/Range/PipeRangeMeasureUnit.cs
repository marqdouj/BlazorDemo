namespace AspireDemo.PIMS.Models.Range
{
    public enum PipeRangeMeasureUnit
    {
        Undefined,
        Millimeter,
        Centimeter,
        Meter,
        Kilometer,
        Inch,
        Foot,
        Yard,
        Mile,
    }

    internal static class PipeRangeMeasureUnitExtensions
    {
        public static string ToSuffix(this PipeRangeMeasureUnit unit)
        {
            return unit switch
            {
                PipeRangeMeasureUnit.Undefined => "?",
                PipeRangeMeasureUnit.Millimeter => "mm",
                PipeRangeMeasureUnit.Centimeter => "cm",
                PipeRangeMeasureUnit.Meter => "m",
                PipeRangeMeasureUnit.Kilometer => "km",
                PipeRangeMeasureUnit.Inch => "in",
                PipeRangeMeasureUnit.Foot => "ft",
                PipeRangeMeasureUnit.Yard => "yd",
                PipeRangeMeasureUnit.Mile => "ml",
                _ => string.Empty,
            };
        }
    }

}
