using LJ.Data.Storage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Blazor.StorageService;

public interface ISessionStorageService
{
   Task<StorageResult<TValue>> GetAsync<TValue>(string key);
    Task SetAsync(string key, object value);
    Task DeleteAsync(string key);
}
