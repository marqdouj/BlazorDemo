using AzureMapsControl.Components.Map;
using Marqdouj.CLRCommon;

namespace AspireDemo.PIMS.Models
{
    public class GPSSettings : StateContainer
    {
        #region MapCamera
        private CameraOptions? _mapCamera;
        public CameraOptions? MapCamera { get => _mapCamera; set => SetValue(ref _mapCamera, value); }
        #endregion

        #region MapStyle
        private StyleOptions? _mapStyle;
        public StyleOptions? MapStyle { get => _mapStyle; set => SetValue(ref _mapStyle, value); }
        #endregion

        #region ShowInstructions
        private bool _showInstructions = true;
        public bool ShowInstructions { get => _showInstructions; set => SetValue(ref _showInstructions, value); }
        #endregion
    }
}
