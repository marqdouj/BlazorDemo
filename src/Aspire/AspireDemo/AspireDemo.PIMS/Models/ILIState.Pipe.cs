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
    }
}
