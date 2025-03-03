﻿using AspireDemo.ApiService.Client.Models;
using System.Net.Http.Json;

namespace AspireDemo.ApiService.Client
{
    public interface IWeatherClient
    {
        Task<IQueryable<WeatherForecast>?> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default);
    }

    internal class WeatherClient(HttpClient httpClient) : IWeatherClient
    {
        public async Task<IQueryable<WeatherForecast>?> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
        {
            List<WeatherForecast>? forecasts = null;

            await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weather-forecast", cancellationToken))
            {
                if (forecasts?.Count >= maxItems)
                {
                    break;
                }
                if (forecast is not null)
                {
                    forecasts ??= [];
                    forecasts.Add(forecast);
                }
            }

            return forecasts?.AsQueryable();
        }
    }
}