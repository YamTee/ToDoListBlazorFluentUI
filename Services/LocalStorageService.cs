namespace ToDoListBlazorFluentUI.Services;

public interface ILocalStorageService
{
    Task<T?> GetValueAsync<T>(string key);
    Task RemoveAsync(string key);
    Task SetValueAsync<T>(string key, T value);
    Task Clear();
}

public class LocalStorageService(IJSRuntime jsRuntime) : IAsyncDisposable, ILocalStorageService
{
    private Lazy<IJSObjectReference> _accessorJsRef = new();
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    private async Task WaitForReference()
    {
        if (!_accessorJsRef.IsValueCreated)
        {
            _accessorJsRef = new(await _jsRuntime
                .InvokeAsync<IJSObjectReference>("import", "./js/localStorage.js"));
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_accessorJsRef.IsValueCreated)
        {
            await _accessorJsRef.Value.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }

    public async Task<T?> GetValueAsync<T>(string key)
    {
        await WaitForReference();
        var value = await _accessorJsRef.Value.InvokeAsync<string>("get", key);

        var result = JsonConvert.DeserializeObject<T>(value);

        return result;
    }

    public async Task SetValueAsync<T>(string key, T value)
    {
        await WaitForReference();

        var jsonValue = JsonConvert.SerializeObject(value);

        await _accessorJsRef.Value.InvokeVoidAsync("set", key, jsonValue);
    }

    public async Task Clear()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("clear");
    }

    public async Task RemoveAsync(string key)
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("remove", key);
    }
}