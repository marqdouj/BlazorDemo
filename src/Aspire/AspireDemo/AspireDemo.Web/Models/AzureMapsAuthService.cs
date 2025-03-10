using AspireDemo.PIMS.Models;
using Microsoft.Identity.Client;
using Microsoft.JSInterop;

namespace AspireDemo.Web.Models
{
    public static class AzureMapsAuthService
    {
        private const string AuthorityFormat = "https://login.microsoftonline.com/{0}/oauth2/v2.0";
        private const string MSGraphScope = "https://atlas.microsoft.com/.default";
        internal static AzureMapsConfig MapConfig { get; } = new();

        public static void SetAzureMapConfig(this WebApplicationBuilder builder)
        {
            var mapSection = builder.Configuration.GetRequiredSection("AzureMaps");
            mapSection.Bind(MapConfig);
        }

        [JSInvokable]
        public static async Task<string> GetMapAccessToken()
        {
            IConfidentialClientApplication daemonClient;
            daemonClient = ConfidentialClientApplicationBuilder.Create(MapConfig.AadAppId)
                .WithAuthority(string.Format(AuthorityFormat, MapConfig.AadTenantId))
                .WithClientSecret(MapConfig.AppKey)
                .Build();
            AuthenticationResult authResult =
            await daemonClient.AcquireTokenForClient([MSGraphScope]).ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}
