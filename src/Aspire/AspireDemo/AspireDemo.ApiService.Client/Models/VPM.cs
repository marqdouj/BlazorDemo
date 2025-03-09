namespace AspireDemo.ApiService.Client.Models
{
    public record VPM(
        int Id,
        Guid Rid,
        string Name,
        double Grade,
        double WallThickness,
        double? Diameter,
        int EPSG,
        string Color);
}
