using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Data.Sort;

/// <summary>
/// 拓扑排序
/// </summary>
/// <typeparam name="T"></typeparam>
public class TopologicalSortNode<T>(T data)
{
    /// <summary>
    /// 数据
    /// </summary>
    public T Data { get; private set; } = data;

    /// <summary>
    /// 一来
    /// </summary>
    public List<TopologicalSortNode<T>> Dependents { get; private set; } = [];

    /// <summary>
    /// 排序
    /// </summary>
    public int IncomingEdges { get; set; } = 0;

    /// <summary>
    /// 添加节点
    /// </summary>
    /// <param name="dependent"></param>
    public void AddDependent(TopologicalSortNode<T> dependent)
    {
        Dependents.Add(dependent);
        dependent.IncomingEdges++;
    }
}