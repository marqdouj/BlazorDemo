namespace AspireDemo.ApiService.Client
{
    public interface IApiServiceClient
    {
        INewsletterClient Newsletter { get; }
        IPIMSClient PIMS { get; }
        IWeatherClient Weather { get; }
    }

    public class ApiServiceClient(HttpClient httpClient) : IApiServiceClient
    {
        public INewsletterClient Newsletter { get; } = new NewsletterClient(httpClient);

        public IPIMSClient PIMS { get; } = new PIMSClient(httpClient);

        public IWeatherClient Weather { get; } = new WeatherClient(httpClient);
    }
}
