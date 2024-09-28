using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Data.Storage;


/// <summary>
/// 持久化结果
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <param name="success"></param>
/// <param name="value"></param>
public readonly struct StorageResult<TValue>(bool success, TValue? value)
{

    /// <summary>
    /// 获取结果
    /// </summary>
    public bool Success { get; } = success;


    /// <summary>
    /// 结果
    /// </summary>
    public TValue? Value { get; } = value;
}
