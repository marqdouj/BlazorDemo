namespace AspireDemo.Web.Common
{
    public interface IServerLocalStorage
    {
        ValueTask<IServerLocalStorageResult<TValue>> GetAsync<TValue>(string key);
        ValueTask SetAsync(string key, object value);
    }

    public interface IServerLocalStorageResult<TValue>
    {
        bool Success { get; }
        public TValue? Value { get; }
    }
}
