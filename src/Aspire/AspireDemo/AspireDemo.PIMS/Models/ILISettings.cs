using Marqdouj.CLRCommon;

namespace AspireDemo.PIMS.Models
{
    public class ILISettings
    {
        internal static int DEFAULT_PAGE_SIZE = 25;

        public MinMaxN<int> PageSize { get; } = new (10, 100, DEFAULT_PAGE_SIZE);
        public GridSettings Grid { get; set; } = new();
        public PipeSettings Pipe { get; set; } = new();
    }
}
