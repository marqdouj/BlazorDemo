namespace AspireDemo.PIMS.Models
{
    public class ILIState
    {
        public int? VpmId { get; set; }
        public int Page { get; set; } = 1;
        public PipeState Pipe { get; set; } = new();
    }
}
