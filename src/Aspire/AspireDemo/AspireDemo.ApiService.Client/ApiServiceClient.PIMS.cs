using AspireDemo.ApiService.Client.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace AspireDemo.ApiService.Client
{
    public interface IPIMSClient
    {
        Task<List<VPMEvent>> VPMEventsAsync(int vpmId, CancellationToken cancellationToken = default);
        Task<List<VPMEventType>> VPMEventTypesAsync(CancellationToken cancellationToken = default);
        Task<List<VPMGps>> VPMGpsAsync(int vpmId, CancellationToken cancellationToken = default);
        Task<List<VPMGpsType>> VPMGpsTypesAsync(CancellationToken cancellationToken = default);
        Task<List<VPM>> VPMsAsync(CancellationToken cancellationToken = default);
    }

    internal class PIMSClient(HttpClient httpClient) : IPIMSClient
    {
        public async Task<List<VPM>> VPMsAsync(CancellationToken cancellationToken = default)
        {
            var data = await httpClient.GetFromJsonAsync<List<VPM>>("/vpm", cancellationToken);
            return data ?? throw new Exception("/vpm failed to return results.");
        }

        public async Task<List<VPMEvent>> VPMEventsAsync(int vpmId, CancellationToken cancellationToken = default)
        {
            var values = new Dictionary<string, string?>
            {
                { nameof(vpmId), vpmId.ToString() },
            };
            var q = QueryHelpers.AddQueryString("/vpm/events", values);
            var data = await httpClient.GetFromJsonAsync<List<VPMEvent>>(q, cancellationToken);
            return data ?? throw new Exception($"/vpm/events/{vpmId} failed to return results.");
        }

        public async Task<List<VPMEventType>> VPMEventTypesAsync(CancellationToken cancellationToken = default)
        {
            var data = await httpClient.GetFromJsonAsync<List<VPMEventType>>("/vpm/events/types", cancellationToken);
            return data ?? throw new Exception("/vpm/events/types failed to return results.");
        }

        public async Task<List<VPMGps>> VPMGpsAsync(int vpmId, CancellationToken cancellationToken = default)
        {
            var values = new Dictionary<string, string?>
            {
                { nameof(vpmId), vpmId.ToString() },
            };
            var q = QueryHelpers.AddQueryString("/vpm/gps", values);
            var data = await httpClient.GetFromJsonAsync<List<VPMGps>>(q, cancellationToken);
            return data ?? throw new Exception($"/vpm/gps/{vpmId} failed to return results.");
        }

        public async Task<List<VPMGpsType>> VPMGpsTypesAsync(CancellationToken cancellationToken = default)
        {
            var data = await httpClient.GetFromJsonAsync<List<VPMGpsType>>("/vpm/gps/types", cancellationToken);
            return data ?? throw new Exception("/vpm/gps/types failed to return results.");
        }
    }
}
