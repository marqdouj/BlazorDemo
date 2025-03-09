namespace AspireDemo.ApiService.Client.Models
{
    public record VPMEvent(
        int Id,
        int VPMId,
        int VPMEventTypeId,
        double DFS,
        double Clock,
        double Length,
        double Width);
}
