using Marqdouj.CLRCommon;

namespace AspireDemo.Web.Components.Pages
{
    internal class CounterState : StateContainer
    {
        #region CurrentCount
        private int currentCount;
        public int CurrentCount { get => currentCount; set => SetValue(ref currentCount, value); }
        #endregion
    }
}
