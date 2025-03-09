using AspireDemo.Web.Common;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace AspireDemo.Web.Models
{
    internal class ServerLocalStorage(ProtectedLocalStorage storage) : IServerLocalStorage
    {
        public async ValueTask<IServerLocalStorageResult<TValue>> GetAsync<TValue>(string key)
        {
            return new ServerLocalStorageResult<TValue>(await storage.GetAsync<TValue>(key));
        }

        public async ValueTask SetAsync(string key, object value)
        {
            await storage.SetAsync(key, value);
        }
    }

    internal readonly struct ServerLocalStorageResult<TValue> : IServerLocalStorageResult<TValue>
    {
        internal ServerLocalStorageResult(ProtectedBrowserStorageResult<TValue> result)
        {
            Success = result.Success;
            Value = result.Value;
        }

        public bool Success { get; }

        public TValue? Value { get; }
    }
}
