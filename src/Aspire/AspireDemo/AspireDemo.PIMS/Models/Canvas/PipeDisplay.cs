using AspireDemo.PIMS.Models.Range;

namespace AspireDemo.PIMS.Models.Canvas
{
    internal class PipeDisplay(string id, PipeSettings settings, PipeState state)
    {
        private readonly PipeSettings settings = settings;
        private readonly PipeState state = state;

        public string Id { get; } = id;
        public List<ColorStop> Background => settings.Background;
        public PipeLine ClockLine { get; set; } = new(ColorName.Red) { LineDash = [5, 3] };
        public string ClockPosition => settings.ClockPosition.ToString();
        public string ClockText => $"{state.Clock} ({state.Channels.Value})";
        public List<PipeEvent> Events { get; set; } = [];
        public PipeLabel Labels { get; set; } = new PipeLabel(ColorName.LightCyan, ColorName.Red);
        public PipeLine RangeLine { get; set; } = new(ColorName.Blue) { LineDash = [5, 3] };
        public PipeLine SelectedBorder { get; set; } = new(ColorName.Red) { LineDash = [1, 2], LineWidth = 5 };

        private int Precision { get; set; } = 2;

        private string FormatS(double value) => value.ToString($"F{Precision}");

        #region Range

        public PipeRange Range => state.GetRange();

        public string X1 { get { return FormatS(Range.X1); } }

        public string X2 { get { return FormatS(Range.X2); } }

        public string XCenter { get { return FormatS(Range.Center); } }

        public string XCenterUnit { get { return string.Format("{0}{1}", XCenter, XUnit); } }

        public string XUnit { get { return Range.Unit.ToSuffix(); } }

        #endregion
    }
}
