using LJ.Blazor.StorageService;
using LJ.Data.Storage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace LJ.Blazor.AntDesign.StorageService;

public class SessionStorageService : ISessionStorageService
{

    private readonly ProtectedSessionStorage _protectedSessionStorage;

    public SessionStorageService(ProtectedSessionStorage protectedSessionStorage)
    {
        _protectedSessionStorage = protectedSessionStorage;
    }

    public async Task<StorageResult<TValue>> GetAsync<TValue>(string key)
    {
        var result = await _protectedSessionStorage.GetAsync<TValue>(key);
        return new StorageResult<TValue>(result.Success, result.Value);
    }


    public async Task SetAsync(string key, object value)
    {
        await _protectedSessionStorage.SetAsync(key, value);
    }

    public async Task DeleteAsync(string key)
    {
        await _protectedSessionStorage.DeleteAsync(key);
    }
}
