using AspireDemo.PIMS.Models.Canvas;
using AspireDemo.PIMS.Models.Clock;

namespace AspireDemo.PIMS.Models
{
    public enum PipeClockPosition
    {
        Left,
        Right,
    }

    public class PipeSettings
    {
        public static readonly int HEIGHT_DEFAULT = 280;
        public static readonly int WIDTH_DEFAULT = 850;
        public static List<ColorStop> BackgroundDefault { get; set; } =
        [
            new ColorStop(0, ColorName.DarkGray),
            new ColorStop(0.5, ColorName.White),
            new ColorStop(1, ColorName.DarkGray)
        ];

        public List<ColorStop> Background { get; set; } = BackgroundDefault;
        public PipeClockPosition ClockPosition { get; set; } = PipeClockPosition.Left;
        public int Height { get; set; } = HEIGHT_DEFAULT;
        public int Width { get; set; } = WIDTH_DEFAULT;

        public double ClockScroll { get; set; } = PipeClocks.DEGREES_PER_CLOCK / 2.0; //default to 1/2 hour)
        public double DFSScroll { get; set; } = 5; 
    }
}
