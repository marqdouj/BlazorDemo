namespace AspireDemo.PIMS.Models
{
    public class ILIConfig(PipeSettings settings, PipeState state)
    {
        public PipeSettings Settings { get; } = settings;
        public PipeState State { get; } = state;

        public void Reset()
        {
            Settings.Reset();
            State.Reset();
        }
    }
}
