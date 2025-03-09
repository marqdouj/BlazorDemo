using AspireDemo.ApiService.Client.Models;
using AspireDemo.PIMS.Models.Clock;
using AspireDemo.PIMS.Models.Range;

namespace AspireDemo.PIMS.Models
{
    internal class VPMEventVM
    {
        private readonly VPMEvent source;

        internal VPMEventVM(VPMEvent source, PipeRangeMeasureUnit unit)
        {
            Id = source.Id;
            Clock = new ClockTime(source.Clock);
            Range = new PipeRange(source, unit);
            Length = PipeClocks.NormalizeDegrees(source.Length);
            this.source = source;
        }

        public int Id { get; }
        public VPMEventGroup Group => source.ToGroup();
        public double Length { get; }
        public ClockTime Clock { get; }
        public PipeRange Range { get; }
        public bool IsSelected { get; set; }
    }
}
