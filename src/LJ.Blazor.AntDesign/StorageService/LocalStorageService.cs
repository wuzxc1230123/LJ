using LJ.Blazor.StorageService;
using LJ.Data.Storage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Blazor.AntDesign.StorageService;

public class LocalStorageService: ILocalStorageService
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;

    public LocalStorageService(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

    public async Task<StorageResult<TValue>> GetAsync<TValue>(string key) 
    {
        var result = await _protectedLocalStorage.GetAsync<TValue>(key);
        return new StorageResult<TValue>(result.Success, result.Value);
    }


    public async Task SetAsync(string key, object value)
    {
        await _protectedLocalStorage.SetAsync(key, value);
    }

    public async Task DeleteAsync(string key)
    {
        await _protectedLocalStorage.DeleteAsync(key);
    }
}
