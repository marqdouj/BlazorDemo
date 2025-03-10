using AspireDemo.PIMS.Models.Canvas;
using Marqdouj.CLRCommon;

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
        public MinMaxN<int> ClockScroll { get; set; } = new MinMaxN<int>(15, 90, 15); //default value 15; 1/2 hour

        public PipeSettings Copy()
        {
            return new PipeSettings
            {
                Background = [.. Background],
                ClockPosition = ClockPosition,
                Height = Height,
                Width = Width,
                ClockScroll = ClockScroll
            };
        }

        public void Update(PipeSettings settings)
        {
            Background = [.. settings.Background];
            ClockPosition = settings.ClockPosition;
            Height = settings.Height;
            Width = settings.Width;
            ClockScroll = settings.ClockScroll;
        }

        public void Reset()
        {
            Background = BackgroundDefault;
            ClockPosition = PipeClockPosition.Left;
            Height = HEIGHT_DEFAULT;
            Width = WIDTH_DEFAULT;
            ClockScroll = new MinMaxN<int>(15, 90, 30);
        }   
    }
}
