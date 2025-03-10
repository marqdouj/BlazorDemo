using AspireDemo.PIMS.Models.Clock;
using AspireDemo.PIMS.Models.Range;
using Marqdouj.CLRCommon;

namespace AspireDemo.PIMS.Models
{
    public class PipeState
    {
        public static readonly double CLOCK_DEFAULT = 180.0; //6 O'Clock (bottom of pipe)

        public ClockTime Clock { get; set; } = new(CLOCK_DEFAULT);
        public MinMaxN<int> Channels { get; set; } = new(1, 12, 12);
        public MinMaxN<double> DFS { get; set; } = new(-10, 10, 0);
        public PipeRangeMeasureUnit Unit { get; set; } = PipeRangeMeasureUnit.Meter;

        public void SetRange(double dfsMin, double dfsMax)
        {
            var dfs = new MinMaxN<double>(dfsMin, dfsMax, dfsMin);
            dfs.Value = dfs.Center;
            DFS = dfs;
        }

        public PipeRange GetRange() => new (DFS.Value, DFS.Width, Unit);

        public PipeState Copy()
        {
            return new PipeState
            {
                Clock = new(Clock.ToDegrees()),
                Channels = new(Channels.Min, Channels.Max, Channels.Value),
                DFS = new(DFS.Min, DFS.Max, DFS.Value),
                Unit = Unit
            };
        }

        public void Update(PipeState state)
        {
            Clock = state.Clock;
            Channels = state.Channels;
            DFS = state.DFS;
            Unit = state.Unit;
        }

        public void Reset()
        {
            Clock = new(CLOCK_DEFAULT);
            Channels = new(1, 12, 12);
            DFS = new(-10, 10, 0);
            Unit = PipeRangeMeasureUnit.Meter;
        }
    }
}
