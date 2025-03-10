using AspireDemo.ApiService.Client.Models;
using AzureMapsControl.Components.Atlas;

namespace AspireDemo.PIMS.Models
{
    internal class VPMListItem(VPM vpm)
    {
        public VPM Vpm { get; } = vpm;
        public string DisplayValue => Vpm.Id.ToString();
        public string DisplayName => Vpm.Name;
        public string LineColor => string.IsNullOrWhiteSpace(Vpm.Color) ? "Khaki" : Vpm.Color;
        public bool IsLoaded => StartPosition is not null;
        public Position? StartPosition { get; set; }
        public string DataSourceId => $"{DataSourcePrefix}{DisplayValue}";
        public string? LayerId { get; set; }
        public static string DataSourcePrefix { get; } = "vpm_";
    }
}
