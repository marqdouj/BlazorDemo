namespace AspireDemo.ApiService.Client.Models
{
    public record VPMGps(
        int Id,
        int VPMId,
        double Longitude,
        double Latitude,
        double? Elevation,
        double DFS,
        int VPMGpsTypeId,
        string Description);
}
